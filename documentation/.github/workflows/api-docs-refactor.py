import os
import re

def replace_br_in_md_files(content):
    new_content = content.replace("<br>", "\n\n").replace("<br/>", "\n\n").replace("<br />", "\n\n")
    return new_content

def add_front_matter(content):
    match = re.search(r"^#\s+(.*)$", content, re.MULTILINE)
    if match:
        header = match.group(1)
        readable_header = header.replace("&lt;", "<").replace("&gt;", ">")
        new_content = "--- \n" + "title: " + readable_header + "\n" + "--- \n" + content
        return new_content
    else:
        return content

def remove_dot_slash_from_links(content):
    pattern = re.compile(r'(\[[^\]]*\])\(\./([^)]+)\)')
    new_content = pattern.sub(r'\1(\2)', content)
    return new_content

if __name__ == "__main__":
    directory = sys.argv[1]
    for root, dirs, files in os.walk(directory):
        for filename in files:
            if filename.endswith(".md"):
                filepath = os.path.join(root, filename)
                print(f"Processing: {filepath}")

                with open(filepath, "r", encoding="utf-8") as file:
                    content = file.read()
                    content = add_front_matter(content)
                    content = replace_br_in_md_files(content)
                    content = remove_dot_slash_from_links(content)

                with open(filepath, "w", encoding="utf-8") as file:
                    file.write(content)

    print("Done processing markdown files.")
