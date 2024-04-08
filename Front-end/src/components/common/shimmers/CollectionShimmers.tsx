import { Skeleton } from "@/components/ui/skeleton";
import clsx from "clsx";

type Props = {
  className?: string;
  elements: number;
};

const CollectionsShimmers = ({ elements, className = "" }: Props) => {
  return (
    <>
      {Array.from({ length: elements }).map((_, index) => (
        <Skeleton
          key={index}
          className={clsx(
            "col-span-1 md:col-span-2 lg:col-span-3 rounded-3xl h-96",
            className,
          )}
        />
      ))}
    </>
  );
};

export default CollectionsShimmers;
