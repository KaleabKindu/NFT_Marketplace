"use client";
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
import { collections as collectionsData } from "@/utils";
import NoData from "@/components/common/NoData";
import Error from "@/components/common/Error";
import { useGetCollectionsQuery } from "@/store/api";
import CollectionsTableShimmers from "@/components/common/shimmers/CollectionsTableShimmers";
import { useSearchParams } from "next/navigation";
import { useEffect, useState } from "react";
import { ICollection } from "@/types";
import Pagination from "@/components/common/Pagination";
import { FILTER } from "@/data";
type Props = {};

const CollectionsList = (props: Props) => {
  const params = useSearchParams();
  const [page, setPage] = useState(1);
  const [total, setTotal] = useState(0);
  const [size, setSize] = useState(12);
  const { data, isFetching, isError, refetch } = useGetCollectionsQuery({
    search: params.get(FILTER.SEARCH) as string,
    min_volume: params.get(FILTER.MIN_PRICE) as string,
    max_volume: params.get(FILTER.MAX_PRICE) as string,
    creator: params.get(FILTER.CREATOR) as string,
    pageNumber: page,
    pageSize: size,
  });
  const [collections, setCollections] = useState<ICollection[]>([]);
  useEffect(() => {
    if (data) {
      setCollections([...collections, ...data.value]);
      setTotal(data.count);
    }
  }, [data]);
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
            <TableHead>Creator</TableHead>
          </TableRow>
        </TableHeader>
        <TableBody className="rounded-3xl">
          {isFetching ? (
            <CollectionsTableShimmers elements={size} />
          ) : isError ? (
            <TableRow>
              <TableCell colSpan={6} className="pt-10">
                <Error retry={refetch} />
              </TableCell>
            </TableRow>
          ) : collections && collections.length > 0 ? (
            <>
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
                        src={
                          collection.avatar || "/collection/collection-pic.png"
                        }
                      />
                      {collection.name}
                    </Link>
                  </TableCell>
                  <TableCell>{collection.floorPrice} ETH</TableCell>
                  <TableCell>{collection.volume} ETH</TableCell>
                  <TableCell>{collection.items}</TableCell>
                  <TableCell>
                    <Link
                      className="flex items-center"
                      href={`${Routes.USER}/${collection.creator.address}`}
                    >
                      <Avatar
                        className="mr-3"
                        src={collection.creator.avatar}
                      />
                      {collection.creator.userName}
                    </Link>
                  </TableCell>
                </TableRow>
              ))}
              {total > size && (
                <TableRow>
                  <TableCell colSpan={6} className="pt-10">
                    <Pagination
                      total={total}
                      currentPage={page}
                      offset={size}
                      setPage={(a: number) => {
                        setPage(a);
                        window.scrollTo({ top: 0, behavior: "smooth" });
                      }}
                    />
                  </TableCell>
                </TableRow>
              )}
            </>
          ) : (
            <TableRow>
              <TableCell colSpan={6} className="pt-10">
                <NoData message="No Collections found" />
              </TableCell>
            </TableRow>
          )}
        </TableBody>
      </Table>
    </div>
  );
};

export default CollectionsList;
