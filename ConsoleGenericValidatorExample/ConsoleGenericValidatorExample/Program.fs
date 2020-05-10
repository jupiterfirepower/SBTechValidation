type Validator<'config, 't> (config: 'config, vfn: 'config -> 't -> Option<'t>) =
    member _.Validate(x:'t) : Option<'t> = vfn config x

let inline mkLimitedValue (x: ^S) : Option< ^T> = (^T: (static member Create: ^S -> Option< ^T>) x)
let inline extract (x:^S) = (^S: (static member Extract: ^S -> ^T) x)
let inline convertTo (x: ^S) : Option< ^T> = (^T: (static member ConvertTo: ^S -> Option< ^T>) x)

type LimitedValue<'v, 'c, 't    when    'v :> Validator<'c, 't>
and     'v : (new: unit -> 'v)> = 
    LimitedValue of 't 
    with
        static member inline Create(x:'t) : Option<LimitedValue<'v, 'c, 't>> =
            x 
            |> (new 'v()).Validate 
            |> Option.map LimitedValue
        static member inline Extract (x : LimitedValue<'x, 'y, 'z> ) = let (LimitedValue s) = x in s
        static member inline ConvertTo(x : LimitedValue<'x, 'y, 'q> ) : Option<LimitedValue<'a, 'b, 'q>> = 
            let (LimitedValue v) = x
            mkLimitedValue v

//let private validate normalize fn v = if fn (normalize v) then Some (normalize v) else None
let private validate fn v = if fn (v) then Some (v) else None
let validateLen len s = validate (fun (s:string) -> s.Length <= len) s
type LenValidator(config) = inherit Validator<int, string>(config, validateLen)
type Size5 () = inherit LenValidator(5) 
type String5 = LimitedValue<Size5, int, string>
type Size20 () = inherit LenValidator(20) 
type String20 = LimitedValue<Size20, int, string>
let validateRange (min,max) v = validate (fun v -> v >= min && v <= max) v
type NumRangeValidator(config) = inherit Validator<int * int, int>(config, validateRange)

type MaxPos100 () = inherit NumRangeValidator(0, 100)
type RangeMinus100To100 () = inherit NumRangeValidator(-100, 100)

type PositiveInt100 = LimitedValue<MaxPos100, int * int, int>
type Minus100To100 = LimitedValue<RangeMinus100To100, int * int, int>
//type EmailValidator () = inherit RegexValidator("(^[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]+\.[a-zA-Z0-9-.]+$)")
//type FsharpVersionValidator() = inherit ListValidator<float>([1.0;2.0;3.0;3.1;4.0;4.1])
//type Minus100To100 = LimitedValue<inherit NumRangeValidator(0, 100), int * int, int>

type Foo = { foo: PositiveInt100; bar: String5 }
type Vat = {country: string; started: int; value: int}
let validateVat _ v = validate (fun _ -> true) v
type VatValidator(config) = inherit Validator<Vat, int>(config, validateVat)
type DE_2007_19() = inherit VatValidator({country = "DE"; started = 2007; value = 19})

[<EntryPoint>]
let main _ =
    let a: Option<PositiveInt100> = mkLimitedValue 100
    let b = a.Value |> extract
    let c = a.Value
    let c1: Option<PositiveInt100> = convertTo c
    let foo = { foo = (mkLimitedValue 50).Value ; bar = (mkLimitedValue "foo").Value}
    printfn "%A" a
    printfn "%A" b
    printfn "%A" c
    printfn "%A" c1
    printfn "%A" foo
    0 // return an integer exit code
