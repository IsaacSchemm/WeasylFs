namespace WeasylFs

type Media = {
    url: string
    mediaid: int option
    links: Map<string, Media list> option
}