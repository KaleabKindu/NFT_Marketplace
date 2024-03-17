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
import { collections } from "@/utils";
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
          {collections.map((collection, index) => (
            <TableRow className="border-0" key={index}>
              <TableCell>{index + 1}</TableCell>
              <TableCell className="font-medium">
                <Link
                  href={`${Routes.COLLECTION}/${collection.id}`}
                  className="flex items-center gap-4 "
                >
                  <Avatar
                    className="w-12 h-12 rounded-md mr-4"
                    src={collection.avatar || "/collection/collection-pic.png"}
                  />
                  {collection.name}
                </Link>
              </TableCell>
              <TableCell>{collection.floor_price} ETH</TableCell>
              <TableCell>{collection.volume} ETH</TableCell>
              <TableCell>{collection.items}</TableCell>
              <TableCell>
                <Link
                  className="flex items-center"
                  href={`${Routes.USER}/${collection.creator.publicAddress}`}
                >
                  <Avatar className="mr-3" src={collection.creator.avatar} />
                  {collection.creator.userName}
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
