namespace WeasylFs.Endpoints

open WeasylFs
open System

module FrontPage =
    type Request() =
        member val Since = Nullable<DateTimeOffset>() with get, set
        member val Count = Nullable<int>() with get, set

        member this.QueryString =
            seq {
                if this.Since.HasValue then
                    yield ("since", WeasylUtil.ToIsoString this.Since.Value)
                if this.Count.HasValue then
                    yield ("count", this.Count.Value.ToString())
            }
            |> WeasylUtil.BuildQueryString

    let AsyncExecute credentials (parameters: Request) =
        sprintf "api/submissions/frontpage?%s" parameters.QueryString
        |> WeasylUtil.CreateRequest credentials
        |> WeasylUtil.AsyncReadJson<Submission list>

    let ExecuteAsync credentials parameters =
        AsyncExecute credentials parameters
        |> Async.StartAsTask