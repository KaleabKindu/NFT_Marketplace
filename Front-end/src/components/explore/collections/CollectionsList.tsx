import { Avatar } from "@/components/common/Avatar";
import {
  Table,
  TableBody,
  TableCell,
  TableHead,
  TableHeader,
  TableRow,
} from "@/components/ui/table";
import { Routes } from "@/routes";
import Link from "next/link";

type Props = {};

const CollectionsList = (props: Props) => {
  return (
    <div>
      <Table className="text-lg">
        <TableHeader className="bg-accent/50">
          <TableRow className="border-0">
            <TableHead>#</TableHead>
            <TableHead>Collection</TableHead>
            <TableHead>Floor Price</TableHead>
            <TableHead>Volume</TableHead>
            <TableHead>Items</TableHead>
            <TableHead>Owner</TableHead>
          </TableRow>
        </TableHeader>
        <TableBody className="rounded-3xl">
          {Array.from({ length: 10 }).map((_, index) => (
            <TableRow className="border-0" key={index}>
              <TableCell>{index + 1}</TableCell>
              <TableCell className="font-medium">
                <Link
                  href={`${Routes.COLLECTION}/yuiowpo`}
                  className="flex items-center gap-4 "
                >
                  <Avatar
                    className="w-12 h-12 rounded-md mr-4"
                    src="/collection/collection-pic.png"
                  />
                  Collection {index + 1}
                </Link>
              </TableCell>
              <TableCell>0.5 ETH</TableCell>
              <TableCell>15 ETH</TableCell>
              <TableCell>{Math.floor(Math.random() * 1000)}</TableCell>
              <TableCell>
                <Link
                  className="flex items-center"
                  href={`${Routes.USER}/0x3278347jnm2332`}
                >
                  <Avatar className="mr-3" />
                  Tony Stark
                </Link>
              </TableCell>
            </TableRow>
          ))}
        </TableBody>
      </Table>
    </div>
  );
};

export default CollectionsList;
