module FsharpToolbox.Pkg.DomainPrimitives.Test.ExtendedQualifierTest

open NUnit.Framework
open FSharpPlus

open FsharpToolbox.Pkg.DomainPrimitives

[<TestFixture>]

type EmailTest () =

  [<Test>]
  member this.``Test that use extended qualifier to get value from DomainPrimitive``() =
    let expectedValue = "test"
    let tmp = Strings.NonEmptyString.create expectedValue |> Result.get
    let value = Strings.NonEmptyString.value tmp
    Assert.AreEqual(expectedValue, value)
