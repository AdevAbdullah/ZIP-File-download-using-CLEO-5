�dzipfil�
"C:\\Windows\\System32\\wininet.dll  �
#C:\\Windows\\System32\\kernel32.dll � 9    9   M 3����
OThe required DLL file failed to load. Ensure the DLL is in your game directory. N �
InternetOpenA   �
InternetOpenUrlA   �
InternetReadFile   �
HttpQueryInfoA   �
CreateFileA  �
	WriteFile  �
GetFileAttributesA  �
 	 �
	 �%c%c%c%c%c%c%c%cpnode11.datanodes.to:8443/d/ssmlvffckae5w4x3slzc66orvhywjnuqbi3uufwmlwtrtlisqdao6uto4tu2nrjmpqabfo22/GTA_IV_-_Complete_Edition_--_fitgirl-repacks.site_--_.part20.rarhttps:// �
 
 �
����	 
  �
	STAY    >File already exists! Press 'Y' to overwrite, or 'N' to cancel. � �  �
WORDFM �����
  
   �  9� �M ����	STAY     2�  �
YM ����� 
 �
"File deleted. Starting download...  �� �����  �
NM �����
Download cancelled.  ��N  ����
     downloadZIP  �  9   M &����
Failed to open connection. �
:���  N      M H����
      	    �  9   M V���  �  9   M �����
*Failed to connect after multiple attempts. �
:���  N �
#Retrying connection... (Attempt %d)   � O��� H��� ����
   �      @
   �  9  �M �����
;Failed to create file %s. Check permissions and disk space.
  �
����  N      �
  �
  �
           �  9   M p���      �
        �
      �
   2�
         � 9     M ����Z   �
 d �
   �
~y~Download Progress: %d%%d  �
         �
����   �
 �
   |��� u��� ;����  ;�  M ����
*Download failed. Incomplete file detected.  
 �
 �
	 �

 N  � �            !�
~g~Download Completed!� �
����  �
:���  �
:���  �
	 �
 �

  T���     �$    9�  M I����  9  /M d����   
  $     ����   �
  $    9�  M ����$   
  
  $     +���$   �
      �  )�   M ����    �����      M ����   @   �
  �
#C:\\Windows\\System32\\kernel32.dll �
CloseHandle  �
     �
  �
"C:\\Windows\\System32\\wininet.dll �
InternetCloseHandle  �
     �
  FLAG   SRC �   {$CLEO}
script_name {name} "dzipfil"
const userAgent = "downloadZIP", NULL = 0, FILE_ATTRIBUTE_NORMAL = 0x80, CREATE_ALWAYS = 2, GENERIC_WRITE = 0x40000000, DELAY = 3000

int dll_wininet = load_dynamic_library "C:\\Windows\\System32\\wininet.dll"
int dll_kernel32 = load_dynamic_library "C:\\Windows\\System32\\kernel32.dll"
if or
    dll_wininet == 0
    dll_kernel32 == 0
then
    print_help_formatted "The required DLL file failed to load. Ensure the DLL is in your game directory."
    terminate_this_script
end

int proc_InternetOpenA = get_dynamic_library_procedure {procName} "InternetOpenA" dll_wininet
int proc_InternetOpenUrlA = get_dynamic_library_procedure {procName} "InternetOpenUrlA" dll_wininet
int proc_InternetReadFile = get_dynamic_library_procedure {procName} "InternetReadFile" dll_wininet
int proc_HttpQueryInfoA = get_dynamic_library_procedure {procName} "HttpQueryInfoA" dll_wininet
int proc_CreateFileA = get_dynamic_library_procedure {procName} "CreateFileA" dll_kernel32
int proc_WriteFile = get_dynamic_library_procedure {procName} "WriteFile" dll_kernel32
int proc_GetFileAttributesA = get_dynamic_library_procedure {procName} "GetFileAttributesA" dll_kernel32

int url_memory = allocate_memory 256
string_format url_memory "%c%c%c%c%c%c%c%cpnode11.datanodes.to:8443/d/ssmlvffckae5w4x3slzc66orvhywjnuqbi3uufwmlwtrtlisqdao6uto4tu2nrjmpqabfo22/GTA_IV_-_Complete_Edition_--_fitgirl-repacks.site_--_.part20.rar" 0x68 0x74 0x74 0x70 0x73 0x3A 0x2F 0x2F
int file_name_mem = allocate_memory 256
extractFilename(url_memory, file_name_mem)

add_text_label {dynamicKey} 'STAY' {text} "File already exists! Press 'Y' to overwrite, or 'N' to cancel."

while true
    wait 250
    if test_cheat "WORDF"
    then
        int file_exists_flag = Memory.CallFunctionReturn(proc_GetFileAttributesA, 1, 0, file_name_mem)
        if file_exists_flag <> -1
        then
            print_help_forever {key} 'STAY'
            while true
                wait 50
                if is_key_pressed KeyCode.Y
                then
                    clear_help
                    delete_file file_name_mem
                    print_help_formatted "File deleted. Starting download..."
                    wait 2000
                    clear_help
                    break
                end
                if is_key_pressed KeyCode.N
                then
                    print_help_formatted "Download cancelled."
                    wait 2000
                    clear_help
                    terminate_this_script
                end
            end
        end

        int handle_internet = Memory.CallFunctionReturn(proc_InternetOpenA, 5, 0, 0, NULL, NULL, 1, userAgent)
        if handle_internet == NULL
        then
            print_help_formatted "Failed to open connection."
            CloseInternet(handle_internet)
            terminate_this_script
        end

        int retry_count = 3
        while retry_count > 0
            int handle_connection = Memory.CallFunctionReturn(proc_InternetOpenUrlA, 6, 0, 0, 0, 0, NULL, url_memory, handle_internet)
            if handle_connection == NULL
            then
                retry_count--
                if retry_count == 0
                then
                    print_help_formatted "Failed to connect after multiple attempts."
                    CloseInternet(handle_connection)
                    terminate_this_script
                end
                print_help_formatted "Retrying connection... (Attempt %d)" retry_count
                wait 3000
            else
                break
            end
        end

        int handle_file = Memory.CallFunctionReturn(proc_CreateFileA, 7, 0, NULL, FILE_ATTRIBUTE_NORMAL, CREATE_ALWAYS, NULL, 0, GENERIC_WRITE, file_name_mem)
        if handle_file == -1
        then
            print_help_formatted "Failed to create file %s. Check permissions and disk space." file_name_mem
            CloseFile(handle_file)
            terminate_this_script
        end

        int content_length = 0, content_length_size = 4
        int ptr_content_length = get_var_pointer content_length
        int ptr_length_size = get_var_pointer content_length_size
        int query_result

        query_result = Memory.CallFunctionReturn(proc_HttpQueryInfoA, 5, 0, NULL, ptr_length_size, ptr_content_length, 0x20000005, handle_connection)
        if query_result == false
        then
            content_length = 0
        end

        int bytes_written = 0, ptr_bytes_written = get_var_pointer bytes_written
        int total_downloaded = 0, bytes_read = 0, ptr_bytes_read = get_var_pointer bytes_read
        int download_buffer, read_result, progress_calc, progress_percent, write_result
        int buffer_size = 8192

        download_buffer = allocate_memory buffer_size

        while true
            wait 50
            read_result = Memory.CallFunctionReturn(proc_InternetReadFile, 4, 0, ptr_bytes_read, 8192, download_buffer, handle_connection)
            if and
                read_result == True
                bytes_read > 0
            then
                total_downloaded += bytes_read
                progress_calc = total_downloaded * 100
                progress_percent = progress_calc / content_length
                print_big_formatted {format} "~y~Download Progress: %d%%" {time} 100 {style} TextStyle.MiddleSmaller {format} progress_percent

                write_result = Memory.CallFunctionReturn(proc_WriteFile, 5, 0, NULL, ptr_bytes_written, bytes_read, download_buffer, handle_file)
                buffer_size = adjustBufferSize(bytes_read)
                free_memory download_buffer
                download_buffer = allocate_memory buffer_size
            else
                break
            end
        end

        if total_downloaded <> content_length
        then
            print_help_formatted "Download failed. Incomplete file detected."
            delete_file file_name_mem
            free_memory download_buffer
            free_memory url_memory
            free_memory file_name_mem
            terminate_this_script
        end

        wait 160

        add_one_off_sound {x} 0.0 {y} 0.0 {z} 0.0 {soundId} ScriptSound.SoundRaceGo
        print_big_formatted {format} "~g~Download Completed!" {time} 2000 {style} TextStyle.MiddleSmaller

        CloseFile(handle_file)
        CloseInternet(handle_connection)
        CloseInternet(handle_internet)
        free_memory url_memory
        free_memory download_buffer
        free_memory file_name_mem
    end
end

function extractFilename(url: int, filename: int)
    int i = 0, last_slash = -1, current_char = read_memory_with_offset {address} url {offset} i {size} 1

    while current_char <> 0x00
        if current_char == 0x2F
        then
            last_slash = i
        end
        i++
        current_char = read_memory_with_offset {address} url {offset} i {size} 1
    end

    int j = 0
    i = last_slash + 1
    current_char = read_memory_with_offset {address} url {offset} i {size} 1
    while current_char <> 0x00
        write_memory_with_offset {address} filename {offset} j {size} 1 {value} current_char
        i++
        j++
        current_char = read_memory_with_offset {address} url {offset} i {size} 1
    end

    write_memory_with_offset {address} filename {offset} j {size} 1 {value} 0x00
end

function adjustBufferSize(bytesRead: int): int
    int bufferSize = 8192
    if bytesRead < 1024
    then
        bufferSize = 4096
    else
        if bytesRead > 4096
        then
            bufferSize = 16384
        end
    end
    return bufferSize
end

function CloseFile(fileHandle: int)
    int dll_kernel32 = load_dynamic_library "C:\\Windows\\System32\\kernel32.dll"
    int proc_CloseHandle = get_dynamic_library_procedure {procName} "CloseHandle" dll_kernel32
    Memory.CallFunction(proc_CloseHandle, 1, 0, fileHandle)
end

function CloseInternet(handle: int)
    int dll_wininet = load_dynamic_library "C:\\Windows\\System32\\wininet.dll"
    int proc_InternetCloseHandle = get_dynamic_library_procedure {procName} "InternetCloseHandle" dll_wininet
    Memory.CallFunction(proc_InternetCloseHandle, 1, 0, handle)
end	  __SBFTR 