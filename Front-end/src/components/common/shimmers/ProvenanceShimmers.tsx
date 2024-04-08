import React from "react";
import { Skeleton } from "@/components/ui/skeleton";
import { TableRow, TableCell } from "@/components/ui/table";
type Props = {};

const ProvenanceShimmers = (props: Props) => {
  return (
    <>
      {Array.from({ length: 20 }).map((_, index) => (
        <TableRow key={index}>
          <TableCell colSpan={5} className="p-0 mt-0">
            <Skeleton className="w-full h-10 mt-3" />
          </TableCell>
        </TableRow>
      ))}
    </>
  );
};

export default ProvenanceShimmers;
