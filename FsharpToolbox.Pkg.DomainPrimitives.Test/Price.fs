module FsharpToolbox.Pkg.DomainPrimitives.Test.Price

open FsharpToolbox.Pkg.DomainPrimitives.Numbers
open FsharpToolbox.Pkg.DomainPrimitives.Objects
open FsharpToolbox.Pkg.FpUtils
open FSharpPlus
open NUnit.Framework

[<TestFixture>]
type PriceTest () =
  [<TestCase(29900, 1692)>]
  [<TestCase(57900, 3277)>]
  [<TestCase(14900, 843)>]
  [<TestCase(29800, 1687)>]
  [<TestCase(24900, 1409)>]
  [<TestCase(49800, 2819)>]

  [<TestCase(37900, 2145)>]
  [<TestCase(47900, 2711)>]
  [<TestCase(57900, 3277)>]

  [<TestCase(323000, 18283)>]
  [<TestCase(574000, 32491)>]
  member _.``Correctly round vat`` (price, vat) =
    let p =
      price
      |> (AmountInCentsIncVat.create >> Result.get)
      |> Price.create
    Assert.AreEqual(vat, p.vatAmountInCents.Value)

  [<TestCase(29900, 20, 23920)>]
  [<TestCase(29900, 19, 24219)>]
  [<TestCase(29900, 90, 2990)>]
  member _.``Correctly get percent of price`` (price, percentDecrease, percentPrice) =
    let p =
      price
      |> (AmountInCentsIncVat.create >> Result.get)
      |> Price.create
      |> fun p -> p.percentDecrease percentDecrease
    Assert.AreEqual(percentPrice, p.amountInCentsIncVat.Value)
