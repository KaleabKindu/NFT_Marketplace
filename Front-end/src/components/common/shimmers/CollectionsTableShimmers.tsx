import { Skeleton } from "@/components/ui/skeleton";
import { TableCell, TableRow } from "@/components/ui/table";

type Props = {
  elements: number;
};

const CollectionsTableShimmers = ({ elements }: Props) => {
  return (
    <>
      {Array.from({ length: elements }).map((_, index) => (
        <TableRow key={index}>
          <TableCell className="p-0 ">
            <Skeleton className="w-10 h-10 mx-auto" />
          </TableCell>
          <TableCell>
            <div className="flex gap-3 items-center">
              <Skeleton className="w-24 h-12 rounded-md" />
              <Skeleton className="w-full h-12" />
            </div>
          </TableCell>
          <TableCell>
            <Skeleton className="w-full h-12" />
          </TableCell>
          <TableCell>
            <Skeleton className="w-full h-12" />
          </TableCell>
          <TableCell>
            <Skeleton className="w-full h-12" />
          </TableCell>
          <TableCell>
            <div className="flex gap-3 items-center">
              <Skeleton className="w-16 h-12 rounded-full" />
              <Skeleton className="w-full h-12" />
            </div>
          </TableCell>
        </TableRow>
      ))}
    </>
  );
};

export default CollectionsTableShimmers;
