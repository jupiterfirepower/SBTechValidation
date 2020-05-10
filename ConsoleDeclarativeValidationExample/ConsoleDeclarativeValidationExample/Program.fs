// Learn more about F# at http://fsharp.org

open System

module Validation =
    open Microsoft.FSharp.Quotations

    type 'e Test = private Test of ('e -> (string*string) option) 
   
    let private CreateValidator (f: 'e -> ('e Test list -> 'e Test list)) =  
        let entries = f Unchecked.defaultof<_> []
        fun entity -> List.choose (fun (Test test) -> test entity) entries

    let private add (propQ:'x Expr) args message fx (xs: 'e Test list) = 
        let propName, eval =
            match propQ with
            | Patterns.PropertyGet (_,p,_) -> p.Name, fun x -> p.GetValue(x,[||])
            | Patterns.Value (_, ty) when ty = typeof<'e> -> "x", box 
            | _ -> failwith "Unsupported expression"
        let test entity =
            let value = eval entity
            if fx (unbox value) then None
            else  Some (propName, String.Format(message, Array.ofList (value::args)))
        Test(test) :: xs
    
    let private email propQ = 
        let regex = Text.RegularExpressions.Regex(@"\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*")
        add propQ [] "Please enter a valid email address" regex.IsMatch
         
    let private required propQ = 
        add propQ [] "Is a required field" (String.IsNullOrWhiteSpace >> not)
    
    let private between propQ min max = 
        add propQ [box min; box max] "Must be at least {2} and great {1}"  
            (fun v -> v >= min && v <= max)
    
    let private cardNumber propQ = 
        add propQ [] "Invlid card number" <| fun (value:string) -> 
            let checkSum index digit =
                let v = (int digit - int '0') * (index % 2 + 1)
                v % 10 + v / 10 % 10
            Seq.forall Char.IsDigit value
            && Seq.sum (Seq.mapi checkSum value) % 10 = 0   

    type Account = 
        private { Email      : string
                  BirthYear  : int
                  CardNumber : string }
    
    let private validateAccount =
        CreateValidator <| fun x -> 
            required   <@ x.Email @> >>
            email      <@ x.Email @> >>
            between    <@ x.BirthYear @> 1920 2000 >>
            required   <@ x.CardNumber @> >>
            cardNumber <@ x.CardNumber @>
    
    let createAccount email birthYear cardNumber = 
        let acc = { Email = email; BirthYear = birthYear; CardNumber = cardNumber }
        let validResult = validateAccount acc
        validResult

// usage
open Validation
    
[<EntryPoint>]
let main _ =
    let acc = createAccount "example#mail.com" 2003 ""
    printfn("Account - %A") acc
    0 // return an integer exit code
