import { Skeleton } from "@/components/ui/skeleton";

type Props = {
  elements: number;
};

const AssetsShimmers = ({ elements }: Props) => {
  return (
    <>
      {Array.from({ length: elements }).map((_, index) => (
        <Skeleton
          key={index}
          className="sm:col-span-2 md:col-span-3 rounded-3xl h-96"
        />
      ))}
    </>
  );
};

export default AssetsShimmers;
