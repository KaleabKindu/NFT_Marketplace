"use client";
import NFTDetailsLeft from "@/components/asset/NFTDetailLeft";
import NFTDetailsRight from "@/components/asset/NFTDetailRight";
import NFTProvenance from "@/components/asset/NFTProvenance";
import MoreFromCollection from "@/components/asset/MoreFromCollection";
import MoreFromCreator from "@/components/asset/MoreFromCreator";
type Props = {
  params: { id: string };
};

const NFTDetail = ({ params }: Props) => {
  return (
    <div className="flex flex-col gap-10 ">
      <div className="flex flex-col lg:flex-row gap-10">
        <NFTDetailsLeft id={params.id} />
        <NFTDetailsRight id={params.id} />
      </div>
      <NFTProvenance />
      <MoreFromCollection />
      <MoreFromCreator />
    </div>
  );
};

export default NFTDetail;
