"use client";

import {
  Table,
  TableBody,
  TableCell,
  TableHead,
  TableHeader,
  TableRow,
} from "@/components/ui/table";

import {
  Accordion,
  AccordionContent,
  AccordionItem,
  AccordionTrigger,
} from "@/components/ui/accordion";
import Link from "next/link";
import { useParams } from "next/navigation";
import ProvenanceShimmers from "../common/shimmers/ProvenanceShimmers";
import Error from "../common/Error";
import NoData from "../common/NoData";
import { useGetProvenanceQuery } from "@/store/api";
import { useInView } from "react-intersection-observer";
import { useEffect, useState } from "react";
import { IProvenance } from "@/types";
import moment from "moment";
import { Routes } from "@/routes";
import { Avatar } from "../common/Avatar";
import { TypographyP } from "../common/Typography";
import { Loader2 } from "lucide-react";

const NFTProvenance = () => {
  const params = useParams();
  const [page, setPage] = useState(1);
  const [total, setTotal] = useState(0);
  const [size, setSize] = useState(12);
  const { data, isLoading, isFetching, isError, refetch } =
    useGetProvenanceQuery({
      id: params.id as string,
      pageNumber: page,
      pageSize: size,
    });
  const [provenances, setProvenances] = useState<IProvenance[]>([]);
  const { ref, inView } = useInView({ threshold: 1 });

  useEffect(() => {
    if (data) {
      setProvenances([...provenances, ...data.value]);
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
      <AccordionItem value="item-1">
        <AccordionTrigger className="bg-accent text-accent-foreground px-5 rounded-t-md">
          Provenance
        </AccordionTrigger>
        <AccordionContent className="h-96 overflow-y-auto hide-scrollbar">
          <Table className="border">
            <TableHeader>
              <TableRow>
                <TableHead>Events</TableHead>
                <TableHead>Price</TableHead>
                <TableHead>From</TableHead>
                <TableHead>To</TableHead>
                <TableHead>Date</TableHead>
              </TableRow>
            </TableHeader>
            <TableBody>
              {isLoading ? (
                <ProvenanceShimmers />
              ) : isError ? (
                <TableRow>
                  <TableCell colSpan={5}>
                    <Error retry={refetch} />
                  </TableCell>
                </TableRow>
              ) : provenances.length > 0 ? (
                <>
                  {provenances.map((provenance) => (
                    <TableRow key={provenance.event}>
                      <TableCell className="font-medium">
                        {provenance.event}
                      </TableCell>
                      <TableCell>{provenance.price}ETH</TableCell>
                      <TableCell>
                        <Link
                          href={`${Routes.USER}/${provenance.from.address}`}
                          className="flex gap-2 items-center"
                        >
                          <Avatar
                            name={provenance.from.userName}
                            src={provenance.from.avatar}
                          />
                          <TypographyP
                            className="whitespace-nowrap text-ellipsis overflow-hidden"
                            text={provenance.from.userName}
                          />
                        </Link>
                      </TableCell>
                      <TableCell>
                        <Link
                          href={`${Routes.USER}/${provenance.to.address}`}
                          className="flex gap-2 items-center"
                        >
                          <Avatar
                            name={provenance.to.userName}
                            src={provenance.to.avatar}
                          />
                          <TypographyP
                            className="whitespace-nowrap text-ellipsis overflow-hidden"
                            text={provenance.to.userName}
                          />
                        </Link>
                      </TableCell>
                      <TableCell>
                        {moment(provenance.date).format("ll")}
                      </TableCell>
                    </TableRow>
                  ))}
                  {isFetching && (
                    <TableRow>
                      <TableCell colSpan={5}>
                        <Loader2 className="mx-auto h-4 w-4 animate-spin" />
                      </TableCell>
                    </TableRow>
                  )}
                  <div ref={ref} />
                </>
              ) : (
                <TableRow>
                  <TableCell colSpan={5}>
                    <NoData message="Asset has no provenance." />
                  </TableCell>
                </TableRow>
              )}
            </TableBody>
          </Table>
        </AccordionContent>
      </AccordionItem>
    </Accordion>
  );
};

export default NFTProvenance;
