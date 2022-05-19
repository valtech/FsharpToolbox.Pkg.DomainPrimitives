module FsharpToolbox.Pkg.DomainPrimitives.Numbers.Validators

let checkPositiveInt (i: int) =
  if i > 0 then Ok i
  else Error (sprintf "integer %i must be greater than 0" i)

let checkNonNegativeInt (i : int) =
  if i >= 0 then Ok i
  else Error (sprintf "integer %i must be greater than or equal to 0" i)

let checkNonNegativeDecimal (i : decimal) =
  if i >= 0m then Ok i
  else Error (sprintf "decimal %f must be greater than or equal to 0" i)

let checkPositiveInt64 (i : int64) =
  if i > 0L then Ok i
  else Error (sprintf "integer %i must be greater than 0" i)

let checkNonNegativeInt64 (i : int64) =
  if i >= 0L then Ok i
  else Error (sprintf "integer %i must be greater than or equal to 0" i)

let checkIsInRange min max (i: int) =
    if i >= min && i <= max then Ok i
    else Error (sprintf "integer %i must be between %i and %i" i min max)

let checkIsInRange64 min max (i: int64) =
    if i >= min && i <= max then Ok i
    else Error (sprintf "integer %i must be between %i and %i" i min max)

let checkIsInPercentRange (i : decimal) =
  if 0m <= i && i <= 1m then Ok i
  else Error (sprintf "decimal %f must be between 0m and 1m" i)

let tryParseInt (s: string) =
    let couldParse, parsedValue = System.Int32.TryParse s
    if couldParse then Ok parsedValue
    else Error (sprintf "string %s must be an int" s)

let tryParseInt64 (s: string) =
    let couldParse, parsedValue = System.Int64.TryParse s
    if couldParse then Ok parsedValue
    else Error (sprintf "string %s must be an int64" s)

let tryParseDecimal (s: string) =
    let couldParse, parsedValue = System.Decimal.TryParse s
    if couldParse then Ok parsedValue
    else Error (sprintf "string %s must be a valid decimal" s)
