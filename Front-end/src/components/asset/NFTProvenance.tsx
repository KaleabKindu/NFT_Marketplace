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
import { provenances } from "@/data";
import Link from "next/link";

const NFTProvenance = () => {
  return (
    <Accordion type="single" collapsible defaultValue="item-1">
      <AccordionItem value="item-1">
        <AccordionTrigger className="bg-accent text-accent-foreground px-5 rounded-t-md">
          Provenance
        </AccordionTrigger>
        <AccordionContent className="">
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
              {provenances.map((provenance) => (
                <TableRow key={provenance.event}>
                  <TableCell className="font-medium">
                    {provenance.event}
                  </TableCell>
                  <TableCell>{provenance.price}</TableCell>
                  <TableCell>
                    <Link href={""}>{provenance.from}</Link>
                  </TableCell>
                  <TableCell>
                    <Link href={""}>{provenance.from}</Link>
                  </TableCell>
                  <TableCell>{provenance.date}</TableCell>
                </TableRow>
              ))}
            </TableBody>
          </Table>
        </AccordionContent>
      </AccordionItem>
    </Accordion>
  );
};

export default NFTProvenance;
