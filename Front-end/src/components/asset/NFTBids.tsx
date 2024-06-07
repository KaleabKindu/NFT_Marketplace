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
import { useParams } from "next/navigation";
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
export default function NFTBids() {
  const params = useParams();
  const [page, setPage] = useState(1);
  const [total, setTotal] = useState(0);
  const [size, setSize] = useState(12);
  const { data, isLoading, isFetching, isError, refetch } = useGetBidsQuery({
    id: params.id as string,
    pageNumber: page,
    pageSize: size,
  });
  const [bids, setBids] = useState<IBid[]>([]);
  const { ref, inView } = useInView({ threshold: 0.3 });

  useEffect(() => {
    if (data && page * size > bids.length) {
      setBids([...bids, ...data.value]);
      setTotal(data.count);
    }
  }, [data]);
  useEffect(() => {
    if (inView && !(page * size >= total)) {
      setPage(page + 1);
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
                <Error retry={refetch} />
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
                          <Avatar src={bid.from.avatar} />
                          <TypographyP
                            text={
                              bid.from.username
                                ? bid.from.username.slice(0, 10)
                                : bid.from.address.slice(2, 7)
                            }
                          />
                        </Link>
                      </TableCell>
                      <TableCell>{moment(bid.date).format("ll")}</TableCell>
                    </TableRow>
                  ))}
                  {isFetching && (
                    <TableRow>
                      <TableCell colSpan={4}>
                        <Loader2 className="mx-auto h-4 w-4 animate-spin" />
                      </TableCell>
                    </TableRow>
                  )}
                </>
              ) : (
                <NoData message="No bids on this asset yet." />
              )}
              <div ref={ref} />
            </TableBody>
          </Table>
        </AccordionContent>
      </AccordionItem>
    </Accordion>
  );
}
