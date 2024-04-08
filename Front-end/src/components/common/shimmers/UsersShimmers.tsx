import { Skeleton } from "@/components/ui/skeleton";

type Props = {
  elements: number;
};

const UsersShimmers = ({ elements }: Props) => {
  return (
    <>
      {Array.from({ length: elements }).map((_, index) => (
        <Skeleton
          key={index}
          className="col-span-12 sm:col-span-6 md:col-span-4 lg:col-span-3 rounded-2xl h-[15rem]"
        />
      ))}
    </>
  );
};

export default UsersShimmers;
