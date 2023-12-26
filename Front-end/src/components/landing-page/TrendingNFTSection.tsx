import TrendingNFTCard from "./TrendingNFTCard"
import { TypographyH2, TypographyH4 } from "../common/Typography"

type Props = {}

const TrendingNFTSection = (props: Props) => {
  return (
    <div className="flex flex-col gap-5">
        <TypographyH2 text='Discover More NFTs'/>
        <TypographyH4 text='Explore New Trending NFTs'/>
        <div className="grid grid-cols-1  md:grid-cols-2 lg:grid-cols-3 items-center gap-5">
            {
                Array.from({length:6}).map((_, index) => 
                    <TrendingNFTCard key={index}/>
                )
            }

        </div>

    </div>
  )
}

export default TrendingNFTSection