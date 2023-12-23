import {
    Avatar as RadixAvatar,
    AvatarFallback,
    AvatarImage,
  } from "@/components/ui/avatar"
  type Props = {
    className?:string
  }
  export function Avatar({ className }:Props) {
    return (
      <RadixAvatar className={className}>
        <AvatarImage src="https://github.com/shadcn.png" alt="@shadcn" />
        <AvatarFallback>CN</AvatarFallback>
      </RadixAvatar>
    )
  }
  