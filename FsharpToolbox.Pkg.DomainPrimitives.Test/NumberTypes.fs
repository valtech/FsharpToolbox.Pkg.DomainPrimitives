module FsharpToolbox.Pkg.DomainPrimitives.Test.NumberTypes

open FsharpToolbox.Pkg.DomainPrimitives.Numbers
open NUnit.Framework
open FSharpPlus

[<SetUp>]
let Setup () =
    ()

[<TestFixture>]
type NumbersValidationTests () =
  [<Test>]
  member this.``Check getting NonNegativeInt this.value member`` () =
    NonNegativeInt.create 5
    |> Result.get
    |> fun x -> x.Value
    |> fun x -> Assert.AreEqual(x, 5)

  [<Test>]
  member this.``Check getting NonNegativeInt.value member`` () =
    NonNegativeInt.create 5
    |> Result.get
    |> fun x -> x.Value
    |> fun x -> Assert.AreEqual(x, 5)

  [<Test>]
  member this.``Check getting NonNegativeInt value members`` () =
    let v1 =
      NonNegativeInt.create 5
      |> Result.get
      |> NonNegativeInt.value
    let v2 =
      NonNegativeInt.create 5
      |> Result.get
      |> fun x -> x.Value

    Assert.AreEqual(v1, v2)
