import { TypographyH2, TypographyP } from "@/components/common/Typography";
import MintForm from "@/components/mint/MintForm";
type Props = {};

const page = (props: Props) => {
  return (
    <div className="flex flex-col gap-8 lg:w-[50%] mx-auto mt-8 lg:mt-16 p-5">
      <div className="flex flex-col gap-3 border-b pb-5">
        <TypographyH2 text="Mint a New Product" />
        <TypographyP text="Once your item is minted you will not be able to change any of its information." />
      </div>
      <MintForm />
    </div>
  );
};

export default page;
