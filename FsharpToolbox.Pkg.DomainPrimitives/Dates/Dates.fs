namespace FsharpToolbox.Pkg.DomainPrimitives.Dates

open System
open Validators

open FsharpToolbox.Pkg.FpUtils.Result
open FsharpToolbox.Pkg.DomainPrimitives.DateTimes.Validators

type SwedishDate = private SwedishDate of NodaTime.LocalDate
  with
  member this.Value =
    this |> fun (SwedishDate x) -> x

module SwedishDate =

  let tzStockholm =
    NodaTime.DateTimeZoneProviders.Tzdb.["Europe/Stockholm"]

  let private parseString (dateString: string) =
   match DateTime.TryParse(dateString) with
    | true, dateTime ->
      NodaTime.LocalDate.FromDateTime dateTime
      |> SwedishDate.SwedishDate |> Ok
    | false, _ ->
      "Date string is not parsable" |> SwedishDateError.NotParsable |> Error

  let private parseDate str =
    str
    |> localDateTimeFromIsoString
    |>! SwedishDateError.NotParsable
    |>> fun x -> x.Date |> SwedishDate
    >>! fun _ -> parseString str

  let create (dateString: string): Result<SwedishDate, SwedishDateError> =
    Ok dateString
    >>= validateDateValueExists
    >>= dateFormatCheck
    >>= parseDate

  let fromInstant (instant: NodaTime.Instant) =
    instant.InZone(tzStockholm).LocalDateTime.Date
    |> SwedishDate

  let fromLocalDate (localDate : NodaTime.LocalDate) =
    localDate |> SwedishDate

  let now () =
    NodaTime.SystemClock.Instance.GetCurrentInstant()
    |> fromInstant

  let value (date : SwedishDate) = date.Value

  let toString (SwedishDate date) =
    date.ToString("yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture)
