let bind fn input = 
    match input with
    | Ok data -> fn data
    | Error _ as err -> err

let validate1 x = if x > 5 then Ok x else Error "Input less than or equal to five"
let validate2 x = if x < 10 then Ok x else Error "Input greater than or equal to ten"
let validate3 x = if x % 2 <> 0 then Ok x else Error "Input is not odd"
   
// Combine the validations. 
// The result should be a function that takes an int parameter
// and returns a Result<int, string> value
let combinedValidation =
    validate1
    >> bind validate2
    >> bind validate3

[<EntryPoint>]
let main _ =
    let firstValidationFailureResult = combinedValidation 1   
    printfn "%A" firstValidationFailureResult
    printfn "%A" (firstValidationFailureResult = Error "Input less than or equal to five") 

    let secondValidationFailureResult = combinedValidation 23  
    printfn "%A" secondValidationFailureResult
    printfn "%A" (secondValidationFailureResult = Error "Input greater than or equal to ten")

    let thirdValidationFailureResult = combinedValidation 8        
    printfn "%A" thirdValidationFailureResult
    printfn "%A" (thirdValidationFailureResult = Error "Input is not odd")

    let successResult = combinedValidation 7 
    printfn "%A" thirdValidationFailureResult
    printfn "%A"(successResult = Ok 7) 

    0 // return an integer exit code
