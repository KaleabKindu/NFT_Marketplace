import { MdWallet } from "react-icons/md";
import { useAccount, useBalance } from "wagmi";
import { TypographyP } from "../common/Typography";
import { Button } from "../ui/button";
import {
  Dialog,
  DialogContent,
  DialogDescription,
  DialogHeader,
  DialogTitle,
  DialogTrigger,
} from "@/components/ui/dialog";
import { useEffect } from "react";
import { Loader2 } from "lucide-react";
import useContractWriteMutation from "@/hooks/useContractWriteMutation";
import { useAppSelector } from "@/store/hooks";
import { useWeb3Modal } from "@web3modal/wagmi/react";
import { useState } from "react";

type SaleModalProps = {
  tokenId: number;
  price: string;
};
export const BuyModal = ({ tokenId, price }: SaleModalProps) => {
  const [showModal, setShowModal] = useState(false);

  const session = useAppSelector((state) => state.auth.session);
  const { open } = useWeb3Modal();

  const { address } = useAccount();
  const { data: balance } = useBalance({ address: address });
  const { writing, writeSuccess, contractWrite } = useContractWriteMutation();
  const handleClose = () => setShowModal(false);
  const handleBuy = () => {
    contractWrite("buyAsset", price, [tokenId]);
  };
  useEffect(() => {
    if (writeSuccess) {
      handleClose();
    }
  }, [writeSuccess]);
  return (
    <Dialog
      open={showModal}
      onOpenChange={(a) => {
        if (!session) {
          open();
          return;
        }
        setShowModal(a);
      }}
    >
      <DialogTrigger asChild>
        <Button className="flex-1 lg:w-[50%] w-full">Buy Now</Button>
      </DialogTrigger>
      <DialogContent>
        <DialogHeader>
          <DialogTitle>Buy Now</DialogTitle>
          <DialogDescription className="flex flex-col gap-5 pt-10">
            <div className="flex items-center justify-between p-2 rounded-lg ">
              <div className="flex gap-3 items-center">
                <MdWallet size={35} />
                <TypographyP className="font-bold" text="Balance" />
              </div>
              <TypographyP
                className="font-bold"
                text={`${parseFloat(balance?.formatted as string).toFixed(2) ?? "0"} ${balance?.symbol ?? "ETH"}`}
              />
            </div>
            <div className="flex flex-col gap-5">
              <div>
                <TypographyP text="Price" />
                <div className="flex items-stretch bg-secondary rounded-md px-2">
                  <TypographyP
                    className="flex-1 px-2 py-1 font-bold"
                    text={parseFloat(price).toFixed(2)}
                  />
                  <TypographyP
                    className="flex-initial border-l-2 px-2 py-1 font-bold"
                    text="ETH"
                  />
                </div>
              </div>
              <Button
                type="button"
                disabled={writing}
                className="rounded-full self-end"
                size="lg"
                onClick={handleBuy}
              >
                {writing ? (
                  <>
                    <Loader2 className="mr-2 h-6 w-6 animate-spin" />
                    Buying
                  </>
                ) : (
                  "Buy"
                )}
              </Button>
            </div>
          </DialogDescription>
        </DialogHeader>
      </DialogContent>
    </Dialog>
  );
};
