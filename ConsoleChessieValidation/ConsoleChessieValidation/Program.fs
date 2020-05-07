open Chessie.ErrorHandling
open System.Text.RegularExpressions

module ConstrainedTypes =
    type Entity = private { id:int; name:string; email:string }

    let private validateId id = if id > 0 then ok id else fail "id not valid"
    let private validateName (name: string) = if name.Length > 1 then ok name else fail "name not valid"
    let private validateEmail (email: string) = 
        let regex = new Regex(@"^[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]+\.[a-zA-Z0-9-.]+$")
        match regex.IsMatch(email) with
        | true -> ok email
        | false -> fail "email not valid"

    let private idResult = fun id -> validateId id
    let private nameResult = fun name -> validateName name
    let private emailResult = fun email -> validateEmail email
    let private create = fun id name email -> { id = id; name = name; email = email }

    let Create id name email = 
        create 
        <!> idResult id
        <*> nameResult name
        <*> emailResult email

    let Create' id (name:string) (email:string) = 
        idResult id 
        >>= fun id -> nameResult name
        >>= fun name -> emailResult email
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
