module FableValidation=
    open FSharp.Core
    open Fable.Validation.Core
    open FsUnit

    type People = private {
        name: string
        age: int
    }  with
        static member Name = "name"
        static member Age = "age"

    let valid = { name="abcd"; age=10 }
    let result = all <| fun t ->

        { name = t.Test People.Name valid.name // call `t.Test fieldName value` to initialize field state
                    |> t.Trim // pipe the field state to rules
                    |> t.NotBlank "name cannot be blank" // rules can contain params and a generic error message
                    |> t.MaxLen 20 "maxlen is {len}"
                    |> t.MinLen 4 "minlen is {len}"
                    |> t.End // call `t.End` to unwrap the validated
                             // and transformed value,
                             // you can use the transformed values to create a new model

          age = t.Test People.Age valid.age
                    |> t.Gt 0 "should greater then {min}"
                    |> t.Lt 200 "shoudld less then {max}"
                    |> t.End }

    Assert.AreEqual (result, Ok(valid))

    // the result type is Result<'T, 'E list>
    let resultmap: Result<People, Map<string, string list>> = all <| fun t ->

        { name = t.Test People.Name "abc"
                    |> t.MaxLen 20 "maxlen is 20"
                    |> t.MinLen 4 "minlen is 4"
                    |> t.End
          age = t.Test People.Age 201
                    |> t.Gt 0 "should greater then 0"
                    |> t.Lt 200 "should less then 200"
                    |> t.End }

open FableValidation

[<EntryPoint>]
let main _ =
    printfn "%A" valid
    printfn "%A" result
    printfn "%A" resultmap
    0 // return an integer exit code
