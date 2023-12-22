'use client'

  import {
    Accordion,
    AccordionContent,
    AccordionItem,
    AccordionTrigger,
  } from "@/components/ui/accordion"
import NFTCard from "../NFTCard"
import { ScrollArea } from "@radix-ui/react-scroll-area"
import { ScrollBar } from "../ui/scroll-area"

  const MoreFromCreator = () => {
    return (
        <Accordion type="single" collapsible>
        <AccordionItem value="item-1">
            <AccordionTrigger className="bg-accent text-accent-foreground px-5 rounded-t-md mb-5">More From Creator</AccordionTrigger>
            <AccordionContent>
            <ScrollArea>
              <div className="flex gap-5">
                {
                      Array.from({length:8}).map((_, index) => 
                      <div key={index} className="shrink-0 w-full md:w-[50%] lg:w-[25%]">
                          <NFTCard/>
                      </div>
                      )
                  }

              </div>
              <ScrollBar className="hidden" orientation="horizontal" />
            </ScrollArea>
            </AccordionContent>
        </AccordionItem>
    </Accordion>

    )
  }

export default MoreFromCreator