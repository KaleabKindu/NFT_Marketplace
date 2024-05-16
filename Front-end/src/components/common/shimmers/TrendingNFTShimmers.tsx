import { Skeleton } from "@/components/ui/skeleton";
import clsx from "clsx";

type Props = {
  className?: string;
  elements: number;
};

const TrendingNFTShimmers = ({ elements, className = "" }: Props) => {
  return (
    <>
      {Array.from({ length: elements }).map((_, index) => (
        <Skeleton
          key={index}
          className={clsx("h-[30rem] rounded-3xl max-w-[35rem]", className)}
        />
      ))}
    </>
  );
};

export default TrendingNFTShimmers;
