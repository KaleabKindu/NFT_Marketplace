import {
  Avatar as RadixAvatar,
  AvatarFallback,
  AvatarImage,
} from "@/components/ui/avatar";
type Props = {
  className?: string;
  src?: string;
};
export function Avatar({
  className,
  src = "https://github.com/shadcn.png",
}: Props) {
  return (
    <RadixAvatar className={className}>
      <AvatarImage src={src} alt="@shadcn" />
      <AvatarFallback>CN</AvatarFallback>
    </RadixAvatar>
  );
}
