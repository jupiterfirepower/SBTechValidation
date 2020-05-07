open Chessie.ErrorHandling
open System.Text.RegularExpressions

module ConstrainedTypes =
    type Entity = private { id:int; name:string; email:string }

    let validateId id = if id > 0 then ok id else fail "id not valid"
    let validateName (name: string) = if name.Length > 1 then ok name else fail "name not valid"
    let validateEmail (email: string) = 
        let regex = new Regex(@"^[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]+\.[a-zA-Z0-9-.]+$")
        match regex.IsMatch(email) with
        | true -> ok email
        | false -> fail "email not valid"

    let Create id name email = 
        let idResult = validateId id
        let nameResult = validateName name
        let emailResult = validateEmail email
    
        let create = fun id name email -> { id = id; name = name; email = email }
    
        create 
        <!> idResult
        <*> nameResult
        <*> emailResult

    let Create' id (name:string) (email:string) = 
        let idResult = validateId id
        let nameResult = validateName name
        let emailResult = validateEmail email
    
        let create = fun id name email -> { id = id; name = name; email = email }
        
        idResult 
        >>= fun id -> nameResult 
        >>= fun name -> emailResult 
        >>= fun email -> ok (create id name email)

open ConstrainedTypes

[<EntryPoint>]
let main _ =
    //let entity = { Entity.id=1; Entity.name="qwq"; Entity.email = "john@example.com" } // The union cases or fields of the type 'Entity' are not accessible from this code location
    let entity = Create 42 "John" "john@example.com"
    printfn "%A" entity 
    let entity = Create -10 "" "johnexample.com" //Bad ["id not valid"; "name not valid"; "email not valid"]
    printfn "%A" (entity)
    printfn "%A" (Create' -1 "John" "foo") //Bad ["id not valid"]
    0 // return an integer exit code
