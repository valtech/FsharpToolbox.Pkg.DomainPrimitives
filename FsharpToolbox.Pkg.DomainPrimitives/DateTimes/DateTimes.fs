namespace FsharpToolbox.Pkg.DomainPrimitives.DateTimes

open System.Globalization
open NodaTime.Text

open FsharpToolbox.Pkg.DomainPrimitives.Dates.Validators
open FsharpToolbox.Pkg.FpUtils
open FsharpToolbox.Pkg.DomainPrimitives.DateTimes.Validators

type SwedishDateTime = private SwedishDateTime of NodaTime.LocalDateTime
  with
  member this.Value =
    this |> fun (SwedishDateTime x) -> x

module SwedishDateTime =

  let private iso8601Pattern = InstantPattern.ExtendedIso

  let value (datetime : SwedishDateTime) : NodaTime.LocalDateTime = datetime.Value

  let toIsoString dateTime =
    dateTime
    |> value
    |> fun localDateTime -> localDateTime.ToString(iso8601Pattern.PatternText, CultureInfo.InvariantCulture)

  let create (dateString : string) : Result<SwedishDateTime, string> =
    Ok dateString
    >>= (validateDateValueExists >> mapError handleError)
    >>= (dateFormatCheck >> mapError handleError)
    >>= localDateTimeFromIsoString
    |>> SwedishDateTime

