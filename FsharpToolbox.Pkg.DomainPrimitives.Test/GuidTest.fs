module FsharpToolbox.Pkg.DomainPrimitives.Test.Guid

open NUnit.Framework

open System
open FsharpToolbox.Pkg.DomainPrimitives.Strings
open FsharpToolbox.Pkg.DomainPrimitives.Strings.Validators
open FsharpToolbox.Pkg.DomainPrimitives.Test.Util

[<SetUp>]
let Setup () =
    ()

[<TestFixture>]
type GuidTest () =

  [<TestCase("00000000-0000-0000-0000-000000000000")>]
  [<TestCase("1977e189-363a-41c1-8a90-a7a9908cc228")>]
  [<TestCase("1977E189-363A-41C1-8A90-A7A9908CC228")>]
  [<TestCase("00000000000000000000000000000000")>]
  [<TestCase("{02eec351-1f9c-41ad-a6cf-31e1811b7821}")>]
  [<TestCase("a26116976fc04045abe3c358f078cc24")>]
  [<TestCase("a26116976fc04045ABE3C358F078cc24")>]
  [<TestCase("{02eec351-1f9c-41ad-a6cf-31e1811b7821}")>]
  [<TestCase("{0x0754f26d,0x88bd,0x4494,{0xb7,0x6d,0x20,0xbe,0x87,0xbb,0x91,0xbb}}")>]
  [<TestCase("{02EEC351-1F9C-41AD-A6CF-31E1811B7821}")>]
  [<TestCase("A26116976FC04045ABE3C358F078CC24")>]
  [<TestCase("{02EEC351-1F9C-41AD-A6CF-31E1811B7821}")>]
  [<TestCase("{0X0754F26D,0X88BD,0X4494,{0XB7,0X6D,0X20,0XBE,0X87,0XBB,0X91,0XBB}}")>]
  member this.``Valid GUID strings returns Ok`` (guid) =
    Assert.IsTrue(isOk (tryParseGuid guid))


  [<TestCase("")>]
  [<TestCase(" ")>]
  [<TestCase("1977e189:363a:41c1:8a90:a7a9908cc228")>]
  [<TestCase("1977e189;363a;41c1;8a90;a7a9908cc228")>]
  [<TestCase("1977e189=363a=41c1=8a90=a7a9908cc228")>]
  [<TestCase("1-A26116976FC04045ABE3C358F078CC24")>]
  [<TestCase("XYZ-{0x0754f26d,0x88bd,0x4494,{0xb7,0x6d,0x20,0xbe,0x87,0xbb,0x91,0xbb}}")>]
  member this.``Invalid GUID strings returns Error`` (name) =
    Assert.IsTrue(isError (tryParseGuid name))

[<TestFixture>]
type GuidCreateTest () =

  [<Test>]
  member _.``Valid template guid returns OK`` () =
    let expectedGuid = "00000000-0000-0000-0000-000000000000"

    let result = Guid.create expectedGuid
    match result with
    | Ok guid -> Assert.AreEqual(Guid.value guid, expectedGuid)
    | Error _ -> Assert.Fail "Should return expectedGuid as OK"

  [<Test>]
  member _.``Valid guid returns OK and corresponds to input`` () =
    let expectedGuid : string = "267b1f7a-f840-485a-8ae5-7c74cd27285f"

    let result = Guid.create expectedGuid
    match result with
    | Ok guid -> Assert.AreEqual(Guid.value guid, expectedGuid)
    | Error _ -> Assert.Fail "Should return expectedGuid as OK"

  [<Test>]
  member _.``Invalid guid returns Error`` () =
    let expectedGuid = "NotValidGuid"

    let result = Guid.create expectedGuid
    match result with
    | Ok _ -> Assert.Fail "Expected Error if Invalid Guid is passed"
    | Error _ -> Assert.Pass()

  [<Test>]
  member _.``Invalid guid 2 returns Error`` () =
    let expectedGuid = String.concat "-" ["00000000-0000-0000-0000-000000000000"; "NotValidGuid"]

    let result = Guid.create expectedGuid
    match result with
    | Ok _ -> Assert.Fail "Expected Error if Invalid Guid is passed"
    | Error _ -> Assert.Pass()
