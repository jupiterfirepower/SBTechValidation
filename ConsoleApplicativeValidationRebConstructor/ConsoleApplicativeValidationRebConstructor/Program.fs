type CustomerId = private CustomerId of int
// rebinding the constructor
let CustomerId id = if id > 0 then
                       Ok (CustomerId id)
                    else
                       Error ["CustomerId must be positive"]

type EmailAddress = private EmailAddress of string
// rebinding the constructor
let EmailAddress email = if System.String.IsNullOrEmpty(email) then
                            Error ["Email must not be empty"]
                         elif email.Contains("@") then
                            Ok (EmailAddress email)
                         else
                            Error ["Email must contain @-sign"]

type CustomerInfo = private {
    id: CustomerId
    email: EmailAddress
 }

let apply fr xr =
    match fr, xr with
    | Ok f, Ok x -> Ok (f x)
    | Error errs, Ok _ -> Error errs
    | Ok _, Error errs -> Error errs
    | Error errs1, Error errs2 -> Error (List.concat [errs1; errs2])

let (<!>) = Result.map
let (<*>) = apply

// applicative version
let createCustomerApplicative id email =
    let createCustomer customerId email =
        { id = customerId; email = email }

    let idResult = CustomerId id
    let emailResult = EmailAddress email

    createCustomer 
    <!> idResult 
    <*> emailResult

[<EntryPoint>]
let main _ =
    let badCustomerWithError = createCustomerApplicative -1 "email"
    printfn "Bad Customer - %A" badCustomerWithError
    let normalCustomer = createCustomerApplicative 10 "test@fmail.com"
    printfn "Customer - %A" normalCustomer

    let tmpCustomerId = (CustomerId -1) 
    printfn "CustomerId - %A" tmpCustomerId
    //let tryCreateBadCustomerWithError = { id = (CustomerId -1); email = EmailAddress "email" } 

    0 // return an integer exit code
