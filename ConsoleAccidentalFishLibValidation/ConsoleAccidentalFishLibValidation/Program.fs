//Simple F# DSL style record validation framework
open AccidentalFish.FSharp.Validation

module DomainModel =
   type Order = private {
        id: string
        title: string
        cost: double
   }

   let private validateOrder = createValidatorFor<Order>() {
         validate (fun o -> o.id) [
             isNotEmpty
             hasLengthOf 36
         ]
         validate (fun o -> o.title) [
             isNotEmpty
             hasMaxLengthOf 128
         ]
         validate (fun o -> o.cost) [
             isGreaterThanOrEqualTo 0.
         ]    
   }

   // public Order constructor with Validation.
   let createOrder id title cost =
       let order = { id = id; title = title; cost = cost }
       match order |> validateOrder with
       | Ok -> Result.Ok(order)
       | _ as err -> Result.Error(err)

open DomainModel

[<EntryPoint>]
let main _ =
    let vorder = createOrder "36467DC0-AC0F-43E9-A92A-AC22C68F25D3" "A valid order" (53 |> double)
    printfn "Valid Order - %A" vorder
    let border = createOrder "36467DC0-AC0F-43E9-A92A-AC22C68F25D3111" "" (-10 |> double)
    printfn "Not Valid Order - %A" border
    0 // return an integer exit code

(*
//Validating Complex Types
type OrderItem =
    {
        productName: string
        quantity: int
    }

type Customer =
    {
        name: string        
    }

type OrderEntity =
    {
        id: string
        customer: Customer
        items: OrderItem list
    }

let validateOrderEntity = createValidatorFor<OrderEntity>() {
    validate (fun o -> o.customer.name) [
        isNotEmpty
        hasMaxLengthOf 128
    ]
}

let validateCustomer = createValidatorFor<Customer>() {
    validate (fun c -> c.name) [
        isNotEmpty
        hasMaxLengthOf 128
    ]
}

let validateOrderEntityWith = createValidatorFor<OrderEntity>() {
    validate (fun o -> o.customer) [
        withValidator validateCustomer
    ]
}

//Conditional Validation
type DiscountOrder = {
    value: int
    discountPercentage: int
}

let discountOrderValidator = createValidatorFor<DiscountOrder>() {
    validateWhen (fun w -> w.value < 100) (fun o -> o.discountPercentage) [
        isEqualTo 0
    ]
    validateWhen (fun w -> w.value >= 100) (fun o -> o.discountPercentage) [
        isEqualTo 10
    ]
    validate (fun o -> o.value) [
        isGreaterThan 0
    ]
}

//Using withValidatorWhen
type DiscountOrderEntity = {
    value: int
    discountPercentage: int
    discountExplanation: string
}

let orderWithDiscount = createValidatorFor<DiscountOrderEntity>() {
    validate (fun o -> o.discountPercentage) [
        isEqualTo 10
    ]
    validate (fun o -> o.discountExplanation) [
        isNotEmpty
    ]
}

let orderWithNoDiscount = createValidatorFor<DiscountOrderEntity>() {
    validate (fun o -> o.discountPercentage) [
        isEqualTo 0
    ]
    validate (fun o -> o.discountExplanation) [
        isEmpty
    ]
}

let discountOrderEntityValidator = createValidatorFor<DiscountOrderEntity>() {
    validate (fun o -> o) [
        withValidatorWhen (fun o -> o.value < 100) orderWithNoDiscount
        withValidatorWhen (fun o -> o.value >= 100) orderWithDiscount            
    ]
    validate (fun o -> o.value) [
        isGreaterThan 0
    ]
}

let validator = createValidatorFor<DiscountOrderEntity>() {
    validate (fun o -> o) [
        withValidatorWhen (fun o -> o.value < 100) (createValidatorFor<DiscountOrderEntity>() {
            validate (fun o -> o) [
                withValidatorWhen (fun o -> o.value < 100) orderWithNoDiscount
                withValidatorWhen (fun o -> o.value >= 100) orderWithDiscount            
            ]
            validate (fun o -> o.value) [
                isGreaterThan 0
            ]
        })
        withValidatorWhen (fun o -> o.value >= 100) (createValidatorFor<DiscountOrderEntity>() {
            validate (fun o -> o.discountPercentage) [
                isEqualTo 10
            ]
            validate (fun o -> o.discountExplanation) [
                isNotEmpty
            ]
        })
    ]
    validate (fun o -> o.value) [
        isGreaterThan 0
    ]
}

//Using A Function
type DiscountOrderTemp = {
    value: int
    discountPercentage: int
    discountExplanation: string
}

let validatorTemp = createValidatorFor<DiscountOrderTemp>() {
    validate (fun o -> o) [
        withFunction (fun o ->
            match o.value < 100 with
            | true -> Ok
            | false -> Errors([
                {
                    errorCode="greaterThanEqualTo100"
                    message="Some error"
                    property = "value"
                }
            ])
        )
    ]
}

// Discriminated Unions
type CustomerId = CustomerId of string

type CustomerEntity =
    {
        customerId: CustomerId
    }

let unwrapCustomerId (CustomerId id) = id
let validatorCustomerEntity = createValidatorFor<CustomerEntity>() {
    validateSingleCaseUnion (fun c -> c.customerId) unwrapCustomerId [
        isNotEmpty
        hasMaxLengthOf 10
    ]
}

//Multiple Case
type MultiCaseUnion =
    | NumericValue of double
    | StringValue of string

type UnionExample =
    {
        value: MultiCaseUnion
    }

let unionValidator = createValidatorFor<UnionExample>() {
    validateUnion (fun o -> o.value) (fun v -> match v with | StringValue s -> Unwrapped(s) | _ -> Ignore) [
        isNotEmpty
        hasMinLengthOf 10
    ]
    
    validateUnion (fun o -> o.value) (fun v -> match v with | NumericValue n -> Unwrapped(n) | _ -> Ignore) [
        isGreaterThan 0.
    ]
}

*)
