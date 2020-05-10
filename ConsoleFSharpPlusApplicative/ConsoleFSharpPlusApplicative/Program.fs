open System
//open FSharpPlus //FSharpPlus Functors and Applicatives
//Extensions for F# -> F#+
type Validation<'a,'b> =
    | Success of 'a
    | Failure of 'b

    with 
        // as Functor
        static member Map (x, f) = 
            match x with
            | Failure e -> Failure e
            | Success a -> Success (f a)
        static member inline (<!>) = Map

        // as Applicative -> <*>
        //static member Return x = Success x  // let lift x = Some x
        static member inline (<*>) (f, x) =
            match (f, x) with
            | Failure e1, Failure e2 -> Failure (e1@e2) //(List.concat [e1;e2]) 
            | Failure e1, Success _  -> Failure e1
            | Success _ , Failure e2 -> Failure e2
            | Success f , Success x  -> Success (f x)

[<EntryPoint>]
let main _ =
    let a : Validation<int,string list> = Success ((+) 1) <*> Success 7      // Success 8
    let b : Validation<int,string list> = Failure ["f1"]  <*> Success 7      // Failure ["f1"]
    let c : Validation<int,string list> = Success ((+) 1) <*> Failure ["f2"] // Failure ["f2"]
    let d : Validation<int,string list> = Failure ["f1"]  <*> Failure ["f2"] // Failure ["f1"; "f2"]

    a |> printfn "%A"
    b |> printfn "%A"
    c |> printfn "%A"
    d |> printfn "%A"

    Console.ReadLine() |> ignore
    
    0 // return an integer exit code
