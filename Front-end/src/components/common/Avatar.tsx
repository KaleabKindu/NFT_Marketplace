import {
  Avatar as RadixAvatar,
  AvatarFallback,
  AvatarImage,
} from "@/components/ui/avatar";
type Props = {
  name?: string;
  className?: string;
  src?: string;
};
export function Avatar({
  name,
  className,
  src = "https://github.com/shadcn.png",
}: Props) {
  return (
    <RadixAvatar className={className}>
      <AvatarImage src={src} alt="@shadcn" />
      <AvatarFallback className="uppercase font-bold bg-background">
        {name?.slice(0, 2)}
      </AvatarFallback>
    </RadixAvatar>
  );
}
