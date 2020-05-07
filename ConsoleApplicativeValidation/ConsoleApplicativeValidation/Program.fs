open System

module ConstrainedTypes =
    // ---------------------------------------------
    // Constrained CustomerId (FP-style)
    // ---------------------------------------------
    type CustomerId = private CustomerId of int

    module CustomerId =
            /// constructor
            let create id =
                if id > 0 then
                   Ok (CustomerId id)
                else
                   Error ["CustomerId must be positive"]

            // function used to extract data since type is private
            let value (CustomerId id) = id

    // ---------------------------------------------
    // Constrained CustomerId (object-oriented style)
    // ---------------------------------------------
    /// Class with constraint id must be > 0
    (*type CustomerId private(id) =
        /// constructor
        static member create id= 
            if id > 0 then
                Ok (CustomerId id)
             else
                Error ["CustomerId must be positive"]
        /// extractor
        member _.Value = id*)

    // ---------------------------------------------
    // Constrained EmailAddress (FP-style)
    // ---------------------------------------------
    type EmailAddress = private EmailAddress of string

    module EmailAddress =
           /// constructor
           let create email =
               if System.String.IsNullOrEmpty(email) then
                  Error ["Email must not be empty"]
               elif email.Contains("@") then
                  Ok (EmailAddress email)
               else
                  Error ["Email must contain @-sign"]

open ConstrainedTypes

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

    let idResult = CustomerId.create id
    let emailResult = EmailAddress.create email

    createCustomer 
    <!> idResult 
    <*> emailResult

[<EntryPoint>]
let main _ =
    let badCustomerWithError = createCustomerApplicative -1 "email"
    printfn "Bad Customer - %A" badCustomerWithError
    let normalCustomer = createCustomerApplicative 10 "test@fmail.com"
    printfn "Customer - %A" normalCustomer

    //CustomerId a not accessible from this code location(not compiled/compilation error - correct implementation)
    //let incCustomerID = (CustomerId -1) 
    // compilation error - can't create without createCustomerApplicative - correct implementation domain model always in correct state by validation rules
    //let tryCreateBadCustomerWithError = { id = CustomerId.create -1; email = EmailAddress.create "email" } 

    0 // return an integer exit code
