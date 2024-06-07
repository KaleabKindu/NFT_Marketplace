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

type SaleModalProps = {
  tokenId: number;
};
export const DeleteAssetModal = ({ tokenId }: SaleModalProps) => {
  const [open, setOpen] = useState(false);
  const { address } = useAccount();
  const { data: balance } = useBalance({ address: address });
  const { isLoading, writing, writeSuccess, contractWrite } =
    useContractWriteMutation();
  const handleClose = () => setOpen(false);
  const handleDelete = () => {
    contractWrite("deleteAsset", undefined, [tokenId]);
  };
  useEffect(() => {
    if (writeSuccess) {
      handleClose();
    }
  }, [writeSuccess]);
  return (
    <Dialog open={open} onOpenChange={(a) => setOpen(a)}>
      <DialogTrigger asChild>
        <Button
          variant={"ghost"}
          className="flex gap-3 hover:text-red-500 font-medium justify-start items-center w-full"
        >
          <MdDelete size={20} />
          <div>Delete</div>
        </Button>
      </DialogTrigger>
      <DialogContent>
        <DialogHeader>
          <DialogTitle>Delete Asset</DialogTitle>
          <DialogDescription className="flex flex-col gap-5 pt-5">
            <div className="flex flex-col gap-5">
              <div>
                <TypographyP
                  className="text-base text-red-500"
                  text="Are you sure you want to delete this asset?"
                />
              </div>
              <div className="flex justify-end gap-5">
                <Button
                  type="button"
                  variant={"ghost"}
                  className="rounded-full self-end"
                  size="lg"
                >
                  Cancel
                </Button>
                <Button
                  type="button"
                  disabled={isLoading}
                  className="rounded-full self-end"
                  size="lg"
                  onClick={handleDelete}
                >
                  {writing ? (
                    <>
                      <Loader2 className="mr-2 h-6 w-6 animate-spin" />
                      Deleting
                    </>
                  ) : (
                    "Delete"
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
