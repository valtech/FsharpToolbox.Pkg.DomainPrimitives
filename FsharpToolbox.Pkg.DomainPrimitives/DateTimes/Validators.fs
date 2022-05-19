module FsharpToolbox.Pkg.DomainPrimitives.DateTimes.Validators

open NodaTime
open NodaTime.Text

let private tzStockholm =
  DateTimeZoneProviders.Tzdb.Item "Europe/Stockholm"

let localDateTimeFromIsoString (dateTimeOffset : string) =
  match OffsetDateTimePattern.ExtendedIso.Parse dateTimeOffset with
  | parseResult when parseResult.Success ->
    parseResult.Value.InZone(tzStockholm).LocalDateTime
    |> Ok
  | parseResult ->
    $"Invalid format for date error: %s{parseResult.Exception.Message}"
    |> Error
