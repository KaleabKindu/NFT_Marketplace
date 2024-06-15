import {
  Accordion,
  AccordionContent,
  AccordionItem,
  AccordionTrigger,
} from "@/components/ui/accordion";
import {
  Table,
  TableBody,
  TableCell,
  TableHead,
  TableHeader,
  TableRow,
} from "@/components/ui/table";
import { useEffect, useState } from "react";
import { useInView } from "react-intersection-observer";
import NoData from "../common/NoData";
import Error from "../common/Error";
import ProvenanceShimmers from "../common/shimmers/ProvenanceShimmers";
import { useGetBidsQuery } from "@/store/api";
import Link from "next/link";
import moment from "moment";
import { IBid } from "@/types";
import { Routes } from "@/routes";
import { Avatar } from "../common/Avatar";
import { TypographyP } from "../common/Typography";
import { Loader2 } from "lucide-react";

type Props = {
  tokenId?: number;
};
export default function NFTBids({ tokenId }: Props) {
  const [page, setPage] = useState(1);
  const [total, setTotal] = useState(0);
  const [size, setSize] = useState(12);
  const [fetchingNextPage, setFetchingNextPage] = useState(false);
  const { data, isLoading, isFetching, isError, refetch } = useGetBidsQuery({
    id: tokenId as number,
    pageNumber: page,
    pageSize: size,
  });
  const [bids, setBids] = useState<IBid[]>([]);
  const { ref, inView } = useInView({ threshold: 1 });

  useEffect(() => {
    if (data) {
      setBids([...data.value]);
      setFetchingNextPage(false);
      setTotal(data.count);
    }
  }, [data]);
  useEffect(() => {
    if (inView && size < total) {
      setSize(size * 2);
      setFetchingNextPage(true);
    }
  }, [inView]);
  return (
    <Accordion type="single" collapsible defaultValue="item-1">
      <AccordionItem value="item-2">
        <AccordionTrigger className="bg-accent text-accent-foreground px-5 rounded-t-md">
          Bids
        </AccordionTrigger>
        <AccordionContent className="h-80 overflow-y-auto hide-scrollbar">
          <Table className="border">
            <TableHeader>
              <TableRow>
                <TableHead>Bid</TableHead>
                <TableHead>Bid in USD</TableHead>
                <TableHead>From</TableHead>
                <TableHead>Date</TableHead>
              </TableRow>
            </TableHeader>
            <TableBody>
              {isLoading ? (
                <ProvenanceShimmers />
              ) : isError ? (
                <TableRow>
                  <TableCell colSpan={4}>
                    <Error retry={refetch} />
                  </TableCell>
                </TableRow>
              ) : bids.length > 0 ? (
                <>
                  {bids.map((bid, index) => (
                    <TableRow key={index}>
                      <TableCell className="font-medium">{`${bid.bid} WETH`}</TableCell>
                      <TableCell>{`$${parseInt(bid.bid) * 3000}`}</TableCell>
                      <TableCell>
                        <Link
                          href={`${Routes.USER}/${bid.from.address}`}
                          className="flex gap-2 items-center"
                        >
                          <Avatar
                            src={bid.from.avatar}
                            name={bid.from.userName}
                          />
                          <TypographyP
                            className="whitespace-nowrap text-ellipsis overflow-hidden"
                            text={bid.from.userName}
                          />
                        </Link>
                      </TableCell>
                      <TableCell>{moment(bid.date).format("ll")}</TableCell>
                    </TableRow>
                  ))}
                  {isFetching && fetchingNextPage && (
                    <TableRow>
                      <TableCell colSpan={4}>
                        <Loader2 className="mx-auto h-4 w-4 animate-spin" />
                      </TableCell>
                    </TableRow>
                  )}
                  <div ref={ref} />
                </>
              ) : (
                <TableRow>
                  <TableCell colSpan={4}>
                    <NoData message="No bids on this asset yet." />
                  </TableCell>
                </TableRow>
              )}
            </TableBody>
          </Table>
        </AccordionContent>
      </AccordionItem>
    </Accordion>
  );
}
