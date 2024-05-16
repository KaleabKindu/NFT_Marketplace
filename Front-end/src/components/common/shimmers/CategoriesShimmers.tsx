import { Skeleton } from "@/components/ui/skeleton";
import clsx from "clsx";

type Props = {
  className?: string;
  elements: number;
};

const CategoriesShimmers = ({ elements, className = "" }: Props) => {
  return (
    <>
      {Array.from({ length: elements }).map((_, index) => (
        <Skeleton key={index} className={clsx("rounded-2xl h-44", className)} />
      ))}
    </>
  );
};

export default CategoriesShimmers;
