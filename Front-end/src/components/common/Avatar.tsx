import {
  Avatar as RadixAvatar,
  AvatarFallback,
  AvatarImage,
} from "@/components/ui/avatar";
import clsx from "clsx";
type Props = {
  name?: string;
  className?: string;
  src?: string;
  dark?: boolean;
};
export function Avatar({ name, className, src, dark }: Props) {
  return (
    <RadixAvatar className={className}>
      <AvatarImage src={src} alt={name} />
      <AvatarFallback
        className={clsx("uppercase font-bold", { "bg-background": dark })}
      >
        {name?.slice(0, 2)}
      </AvatarFallback>
    </RadixAvatar>
  );
}
