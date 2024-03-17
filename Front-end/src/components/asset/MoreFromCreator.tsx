"use client";

import {
  Accordion,
  AccordionContent,
  AccordionItem,
  AccordionTrigger,
} from "@/components/ui/accordion";
import NFTCard from "../explore/assets/NFTCard";
import { ScrollArea, ScrollBar } from "../ui/scroll-area";
import { assets } from "@/utils";

const MoreFromCreator = () => {
  return (
    <Accordion type="single" collapsible defaultValue="item-1">
      <AccordionItem value="item-1">
        <AccordionTrigger className="bg-accent text-accent-foreground px-5 rounded-t-md mb-5">
          More From Creator
        </AccordionTrigger>
        <AccordionContent>
          <ScrollArea>
            <div className="flex gap-5">
              {assets.map((asset, index) => (
                <div
                  key={index}
                  className="shrink-0 w-full md:w-[50%] lg:w-[25%]"
                >
                  <NFTCard asset={asset} />
                </div>
              ))}
            </div>
            <ScrollBar className="hidden" orientation="horizontal" />
          </ScrollArea>
        </AccordionContent>
      </AccordionItem>
    </Accordion>
  );
};

export default MoreFromCreator;
