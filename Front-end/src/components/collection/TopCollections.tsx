import CollectionCard from "../CollectionCard"
import { TypographyH2, TypographyH4 } from "../common/Typography"
import { ScrollArea, ScrollBar } from "@/components/ui/scroll-area"

type Props = {}

const TopCollections = (props: Props) => {
  return (
    <div className="flex flex-col gap-5">
        <TypographyH2 text='Top Collections'/>
        <TypographyH4 text='Discover the new creative economy'/>
        <ScrollArea>
            <div className="flex gap-6">
                {
                    Array.from({length:6}).map((_, index) => 
                    <div key={index} className="shrink-0 w-full md:w-[50%] lg:w-[26%]">
                        <CollectionCard key={index}/>
                    </div>
                    )
                }

            </div>
            <ScrollBar className="hidden" orientation="horizontal" />
        </ScrollArea>

    </div>
  )
}

export default TopCollections