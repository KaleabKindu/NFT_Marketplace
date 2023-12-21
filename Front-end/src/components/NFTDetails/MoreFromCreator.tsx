'use client'

  import {
    Accordion,
    AccordionContent,
    AccordionItem,
    AccordionTrigger,
  } from "@/components/ui/accordion"
import NFTCard from "../NFTCard"

  const MoreFromOwner = () => {
    return (
        <Accordion type="single" collapsible>
        <AccordionItem value="item-1">
            <AccordionTrigger className="bg-accent text-accent-foreground px-5 rounded-t-md mb-5">More From Owner</AccordionTrigger>
            <AccordionContent className="">
            <div className="flex overflow-x-auto gap-5">
                {
                    Array.from({length:8}).map((_, index) => 
                    <div key={index} className="shrink-0 w-full md:w-[50%] lg:w-[32%]">
                        <NFTCard/>
                    </div>
                    )
                }

            </div>
            </AccordionContent>
        </AccordionItem>
    </Accordion>

    )
  }

export default MoreFromOwner