// Learn more about F# at http://fsharp.org

open System
open System.Text.RegularExpressions

module ActivePatternApplicativeValidation=

    type User = {   
        Name : string
        Password : string
        Email : string
        DateOfBirth : string
    }

    type ValidationError = 
        | InvalidName
        | InvalidPassword
        | InvalidEmail
        | InvalidDateOfBirth

    type ValidatedUser = private {   
        Name : string
        Password : string
        Email : string
        DateOfBirth : DateTime
    }

    let private (|ParseRegex|_|) regex str =
        let m = Regex(regex).Match(str)
        if m.Success then Some (List.tail [ for x in m.Groups -> x.Value ])
        else None
 
    let private (|IsValidName|_|) input =
        if input <> String.Empty then Some () else None

    let private (|IsValidPassword|_|) input =
         if input <> String.Empty && input.Length > 5 then Some () else None
 
    let private (|IsValidEmail|_|) input =
        match input with
        | ParseRegex ".*?@(.*)" [ _ ] -> Some input
        | _ -> None
 
    let private (|IsValidDate|_|) (input:string) =
        match DateTime.TryParse input with
        | true, dt -> Some(dt)
        | _ -> None
 
    let private validateName input = // string -> Result<string, ValidationFailure list>
        match input with
        | IsValidName -> Ok input
        | _ -> Error [ InvalidName ]

    let private validatePassword input = // string -> Result<string, ValidationFailure list>
        match input with
        | IsValidPassword -> Ok input
        | _ -> Error [ InvalidPassword ]
 
    let private validateEmail input = // string -> Result<string, ValidationFailure list>
        match input with
        | IsValidEmail email -> Ok email
        | _ -> Error [ InvalidEmail ]
 
    let private validateDateOfBirth input = // string -> Result<DateTime, ValidationFailure list>
        match input with
        | IsValidDate dob -> Ok dob //Add logic for DOB
        | _ -> Error [ InvalidDateOfBirth ]
 
    let private apply fResult xResult = // Result<('a -> 'b), 'c list> -> Result<'a,'c list> -> Result<'b,'c list>
        match fResult,xResult with
        | Ok f, Ok x -> Ok (f x)
        | Error ex, Ok _ -> Error ex
        | Ok _, Error ex -> Error ex
        | Error ex1, Error ex2 -> Error (List.concat [ex1; ex2])
 
    let private (<!>) = Result.map
    let private (<*>) = apply
 
    let private create name password email (dateOfBirth:DateTime) =
         { Name = name; Password = password; Email = email; ValidatedUser.DateOfBirth = dateOfBirth }
 
    let validateUserTo (input:User) : Result<ValidatedUser, ValidationError list> =
         let validatedName = input.Name |> validateName
         let validatedPassword = input.Password |> validatePassword
         let validatedEmail = input.Email |> validateEmail
         let validatedDateOfBirth = input.DateOfBirth |> validateDateOfBirth
         
         create 
         <!> validatedName 
         <*> validatedPassword 
         <*> validatedEmail 
         <*> validatedDateOfBirth

open ActivePatternApplicativeValidation

[<EntryPoint>]
let main _ =
   
    let notValidTest = 
        let actual = validateUserTo { Name = ""; Password=""; Email = "hallo"; DateOfBirth = "" }
        let expected = Error [ InvalidName; InvalidPassword; InvalidEmail; InvalidDateOfBirth ]
        expected = actual

    let actual = validateUserTo { Name = "John"; Password="123456"; Email = "hallo@sample.com"; DateOfBirth = "2002-03-08" } 
    printfn "%A" actual
    printfn "%A" notValidTest
    0 // return an integer exit code
