import { useState } from "react";
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
import { MdDelete } from "react-icons/md";
import { useAppDispatch } from "@/store/hooks";
import { webApi } from "@/store/api";

type CancelAuctionModalProps = {
  tokenId: number;
  auctionId: number;
};
export const CancelAuctionModal = ({
  tokenId,
  auctionId,
}: CancelAuctionModalProps) => {
  const [open, setOpen] = useState(false);
  const { address } = useAccount();
  const dispatch = useAppDispatch();
  const { data: balance } = useBalance({ address: address });
  const { isLoading, writing, writeSuccess, contractWrite } =
    useContractWriteMutation();
  const handleClose = () => setOpen(false);
  const handleCancel = () => {
    contractWrite("cancelAuction", undefined, [tokenId, auctionId]);
  };
  useEffect(() => {
    if (writeSuccess) {
      dispatch(
        webApi.util.invalidateTags(["NFTs", { id: tokenId, type: "NFTs" }]),
      );
      handleClose();
    }
  }, [writeSuccess]);
  return (
    <Dialog open={open} onOpenChange={(a) => setOpen(a)}>
      <DialogTrigger asChild>
        <Button type="button" className="flex-1 lg:w-[50%] bg-red-500 w-full">
          Cancel Auction
        </Button>
      </DialogTrigger>
      <DialogContent>
        <DialogHeader>
          <DialogTitle>Cancel Auction</DialogTitle>
          <DialogDescription className="flex flex-col gap-5 pt-5">
            <div className="flex flex-col gap-5">
              <div>
                <TypographyP
                  className="text-base text-red-500"
                  text="Are you sure you want to cancel the auction?"
                />
              </div>
              <div className="flex justify-end gap-5">
                <Button
                  type="button"
                  variant={"ghost"}
                  className="rounded-full self-end"
                  size="lg"
                >
                  No
                </Button>
                <Button
                  type="button"
                  disabled={isLoading}
                  className="rounded-full self-end"
                  size="lg"
                  onClick={handleCancel}
                >
                  {writing ? (
                    <Loader2 className="mr-2 h-6 w-6 animate-spin" />
                  ) : (
                    "Yes"
                  )}
                </Button>
              </div>
            </div>
          </DialogDescription>
        </DialogHeader>
      </DialogContent>
    </Dialog>
  );
};
