import { useState } from "react";
import { Dropzone, FileMosaic, ExtFile } from "@files-ui/react";
import useWebTheme from "@/hooks/useWebTheme";

type Props = {
  onChange: (a: any[]) => void;
};
const MultipleFilesUpload = ({ onChange }: Props) => {
  const { mode } = useWebTheme();
  const [files, setFiles] = useState<ExtFile[]>([]);
  const updateFiles = (incommingFiles: ExtFile[]) => {
    setFiles(incommingFiles);
    onChange(incommingFiles.map((obj) => obj.file));
  };
  const removeFile = (id?: string | number) => {
    setFiles(files.filter((x) => x.id !== id));
  };
  return (
    <div>
      <Dropzone
        onChange={updateFiles}
        className="bg-secondary max-h-[200px] overflow-y-auto"
        label="Click or Drag'n Drop Files"
        style={{ border: 0 }}
        header={false}
        footer={false}
        value={files}
      >
        {files.map((file) => (
          <FileMosaic
            darkMode={mode === "dark"}
            key={file.id}
            {...file}
            onDelete={removeFile}
            info
          />
        ))}
      </Dropzone>
    </div>
  );
};

export default MultipleFilesUpload;
