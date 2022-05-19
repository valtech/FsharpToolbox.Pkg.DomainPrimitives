namespace FsharpToolbox.Pkg.DomainPrimitives.DateTimes

open FsharpToolbox.Pkg.DomainPrimitives.Dates.Validators
open NodaTime.Text
open FsharpToolbox.Pkg.FpUtils

module NodaTime =
  open NodaTime

  type Instant with

    static member value inst: string =
      InstantPattern.ExtendedIso.Format inst

    static member private fromFallBackString message (dateString: string) : Result<Instant, string> =
      match System.DateTimeOffset.TryParse dateString with
      | true, result ->
        NodaTime.Instant.FromDateTimeOffset result |> Ok
      | false, _ ->
        sprintf "Invalid format, error: %s" message
        |> Error

    static member private fromIsoString (dateString : string) : Result<Instant, string> =
      match InstantPattern.ExtendedIso.Parse dateString with
      | parseResult when parseResult.Success ->
        parseResult.Value |> Ok
      | parseResult ->
        Instant.fromFallBackString parseResult.Exception.Message dateString

    static member create (dateString : string) : Result<Instant, string> =
      Ok dateString
      >>= (validateDateValueExists >> mapError handleError)
      >>= (dateFormatCheck >> mapError handleError)
      >>= Instant.fromIsoString
