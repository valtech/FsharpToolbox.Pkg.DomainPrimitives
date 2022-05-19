namespace FsharpToolbox.Pkg.DomainPrimitives.Strings

open FsharpToolbox.Pkg.DomainPrimitives.Strings.Validators
open FsharpToolbox.Pkg.FpUtils

[<AutoOpen>]
module System =
  open System

  type Guid with
    static member create (unvalidatedGuid : string) : Result<Guid, string> =
      unvalidatedGuid
      |> checkNotEmpty
      |>! sprintf "Could not create GUID, reason: %s"
      >>= tryParseGuid


    static member value (guid : Guid) : string =
      guid.ToString("D")
