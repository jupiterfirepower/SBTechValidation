// F# for fun and profit: fsharpforfunandprofit.com
// Пример из сайта, собраный и оформленный в консольное приложение.

type Result<'a> =
| Success of 'a
| Failure of string list

type CustomerId = CustomerId of int
type EmailAddress = EmailAddress of string

type CustomerInfo = {
    id: CustomerId
    email: EmailAddress
 }

module Result =
    let map f xResult =
        match xResult with
        | Success x ->
                Success (f x)
        | Failure errs ->
                Failure errs
    // Signature: ('a -> 'b) -> Result<'a> -> Result<'b>
    // "return" is a keyword in F#, so abbreviate it
    let retn x =
        Success x
        // Signature: 'a -> Result<'a>
    let apply fResult xResult =
            match fResult,xResult with
            | Success f, Success x ->
                Success (f x)
            | Failure errs, Success x ->
                Failure errs
            | Success f, Failure errs ->
                Failure errs
            | Failure errs1, Failure errs2 ->
            // concat both lists of errors
            Failure (List.concat [errs1; errs2])
            // Signature: Result<('a -> 'b)> -> Result<'a> -> Result<'b>
    let bind f xResult =
        match xResult with
        | Success x ->
            f x
        | Failure errs ->
            Failure errs
    // Signature: ('a -> Result<'b>) -> Result<'a> -> Result<'b>

[<EntryPoint>]
let main _ =
   
    let createCustomerId id =
      if id > 0 then
         Success (CustomerId id)
      else
         Failure ["CustomerId must be positive"]
   
    // int -> Result<CustomerId>
    let createEmailAddress str =
        if System.String.IsNullOrEmpty(str) then
           Failure ["Email must not be empty"]
        elif str.Contains("@") then
           Success (EmailAddress str)
        else
           Failure ["Email must contain @-sign"]
   
    let createCustomer customerId email =
        { id=customerId; email=email }
   
    let (<!>) = Result.map
    let (<*>) = Result.apply

    // applicative version
    let createCustomerResultA id email =
        let idResult = createCustomerId id
        let emailResult = createEmailAddress email
        createCustomer <!> idResult <*> emailResult
   
    let goodId = 1
    let badId = 0
    let goodEmail = "test@example.com"
    let badEmail = "example.com"
    
    let goodCustomerA =
        createCustomerResultA goodId goodEmail
    printfn "Good Customer - %A" goodCustomerA
    // Result<CustomerInfo> =
    //   Success {id = CustomerId 1; email = EmailAddress "test@example.com";}
    
    let badCustomerA =
        createCustomerResultA badId badEmail
    printfn "Bad Customer - %A" badCustomerA
    // Result<CustomerInfo> =
    //   Failure ["CustomerId must be positive"; "Email must contain @-sign"]

    // В данный пример стоит дописать несколько строк кода в качестве дополнительного примера.
    // Используем функцию createCustomer customerId email
    let badCustomerWithNoErrors = createCustomer (CustomerId -1) (EmailAddress "email")
    printfn "Bad Customer(incorrect domain model state) - %A" badCustomerWithNoErrors
    (*{ id = CustomerId -1
        email = EmailAddress "email" } 
        not Result<CustomerInfo> no Success no Failure
        по сути hacked - нет ни валидации, ни правильного состояния модели предментой области, 
        т.е. креш всех изложенных теоретических идей также ошибки, експшины в последующем коде.
        *)
    // Мы можем вернутся в самый верх примера 
    (*type CustomerId = CustomerId of int
    type EmailAddress = EmailAddress of string
    
    type CustomerInfo = {
        id: CustomerId
        email: EmailAddress
     }*)
    // и также просто написать 
    let badCustomerWithNoErrors = { id = (CustomerId -1); email = (EmailAddress "email") } 
    //let badCustomerWithNoError = { CustomerInfo.id =(CustomerId -1); CustomerInfo.email = (EmailAddress "email") }
    printfn "Bad Customer(incorrect domain model state) - %A" badCustomerWithNoErrors
    (*{ id = CustomerId -1
    email = EmailAddress "email" } 
    not Result<CustomerInfo> no Success no Failure
    то же самое hacked - нет ни валидации, ни правильного состояния модели предментой области, 
    все тоже  - креш всех изложенных теоретических идей также ошибки, експшины в последующем коде.

    то есть в реалии у нас из полезного кода
    
    type CustomerId = CustomerId of int
    type EmailAddress = EmailAddress of string
    
    type CustomerInfo = {
        id: CustomerId
        email: EmailAddress
     }

    но и тот до конца не корректный

    *)
    let bad = Ok { id = (CustomerId -1); email = (EmailAddress "email") } 

    let logic x =
        match x with
        | Ok c -> printfn("%A") c
        | Error e -> printfn("%A") e

    logic bad

    let bad = Error { id = (CustomerId -1); email = (EmailAddress "email") } 
    logic bad

    0 // return an integer exit code
