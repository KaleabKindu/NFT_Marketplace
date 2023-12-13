import {
    Avatar as RadixAvatar,
    AvatarFallback,
    AvatarImage,
  } from "@/components/ui/avatar"
  
  export function Avatar() {
    return (
      <RadixAvatar>
        <AvatarImage src="https://github.com/shadcn.png" alt="@shadcn" />
        <AvatarFallback>CN</AvatarFallback>
      </RadixAvatar>
    )
  }
  