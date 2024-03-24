import { cn } from "@/lib/utils";
import { ReactNode } from "react";

type Props = {
  text: string | ReactNode;
  className?: string;
};
export function TypographyH1({ text, className }: Props) {
  return (
    <h1
      className={cn(
        "scroll-m-20 text-4xl font-extrabold tracking-tight lg:text-5xl ",
        className,
      )}
    >
      {text}
    </h1>
  );
}

export function TypographyH2({ text, className }: Props) {
  return (
    <h2
      className={cn(
        "scroll-m-20  text-3xl font-semibold tracking-tight ",
        className,
      )}
    >
      {text}
    </h2>
  );
}
export function TypographyH3({ text, className }: Props) {
  return (
    <h3
      className={cn(
        "scroll-m-20 text-2xl font-semibold tracking-tight ",
        className,
      )}
    >
      {text}
    </h3>
  );
}
export function TypographyH4({ text, className }: Props) {
  return (
    <h4
      className={cn(
        "scroll-m-20 text-xl font-semibold tracking-tight ",
        className,
      )}
    >
      {text}
    </h4>
  );
}
export function TypographyP({ text, className }: Props) {
  return (
    <p className={cn("text-foreground/50 leading-7 ", className)}>{text}</p>
  );
}
export function TypographyLarge({ text, className }: Props) {
  return <div className={cn("text-lg font-semibold ", className)}>{text}</div>;
}
export function TypographySmall({ text, className }: Props) {
  return (
    <small
      className={cn(
        "text-foreground/50 text-sm font-medium leading-none ",
        className,
      )}
    >
      {text}{" "}
    </small>
  );
}
