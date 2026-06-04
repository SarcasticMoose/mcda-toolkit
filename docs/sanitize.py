import json
import re
import sys
from pathlib import Path


def sanitize_folder_name(name: str) -> str:
    name = re.sub(r'-(\d+)$', lambda m: f'T{m.group(1)}', name)
    return name


def sanitize_filename(name: str) -> str:
    name = re.sub(r'\.md\.md$', '.md', name)
    name = re.sub(r'-(\d+)(\.\w+)$', lambda m: f'T{m.group(1)}{m.group(2)}', name)
    name = re.sub(r'<(\w+)>', r'\1', name)
    return name


def sanitize_links(line: str) -> str:
    def fix_link(m):
        full = m.group(0)
        full = re.sub(r'\.md\.md', '.md', full)
        full = re.sub(r'&lt;(\w+)&gt;', r'\1', full)
        full = re.sub(r'<(\w+)>', r'\1', full)
        full = re.sub(r'-(\d+)(\.md|/)', lambda m: f'T{m.group(1)}{m.group(2)}', full)
        return full

    return re.sub(r'\([^)]+\.md[^)]*\)', fix_link, line)


def sanitize_headings(line: str) -> str:
    if re.match(r'^#{1,6}\s', line):
        line = line.replace('&lt;', '<').replace('&gt;', '>')
        line = re.sub(r'<(\w+)>', r'`<\1>`', line)
    return line


def generic_label(name: str) -> str | None:
    label = re.sub(r'T1(?=\b|[A-Z]|$)', '<T>', name)
    label = re.sub(r'T(\d+)(?=\b|[A-Z]|$)', r'<T\1>', label)
    return label if label != name else None


def get_frontmatter_label(filename: str) -> str | None:
    stem = Path(filename).stem
    return generic_label(stem)


def inject_frontmatter(content: str, label: str) -> str:
    frontmatter = f'---\nsidebar_label: "{label}"\n---\n\n'
    if content.startswith('---'):
        end = content.index('---', 3)
        existing = content[3:end].strip()
        if 'sidebar_label' not in existing:
            new_block = f'---\n{existing}\nsidebar_label: "{label}"\n---\n'
            return new_block + content[end + 3:].lstrip()
        return content
    return frontmatter + content


def sanitize_content(text: str, filename: str) -> str:
    lines = text.split('\n')
    result = []
    in_code_block = False

    for line in lines:
        if re.match(r'^\s*```', line):
            in_code_block = not in_code_block
            result.append(line)
            continue

        if in_code_block:
            result.append(line)
            continue

        if line.strip().startswith('|'):
            line = line.replace('{', '&#123;').replace('}', '&#125;')

        line = sanitize_links(line)
        line = sanitize_headings(line)
        result.append(line)

    content = '\n'.join(result)

    label = get_frontmatter_label(filename)
    if label:
        content = inject_frontmatter(content, label)

    return content


def create_category_json(folder_path: Path, api_root: Path) -> None:
    label = generic_label(folder_path.name)
    if not label:
        return

    category_file = folder_path / '_category_.json'
    if category_file.exists():
        return

    data: dict = {"label": label}

    main_doc = folder_path / f'{folder_path.name}.md'
    if main_doc.exists():
        rel = main_doc.relative_to(api_root).with_suffix('')
        data["link"] = {
            "type": "doc",
            "id": str(rel).replace('\\', '/')
        }

    category_file.write_text(json.dumps(data, indent=2), encoding='utf-8')
    print(f"✓ [cat]  {folder_path.name}/_category_.json -> label: {label}")


def sanitize_folder(folder: str):
    path = Path(folder)

    dirs = sorted(path.rglob('*'), key=lambda p: -len(p.parts))
    for d in dirs:
        if d.is_dir():
            new_name = sanitize_folder_name(d.name)
            if new_name != d.name:
                new_path = d.with_name(new_name)
                d.rename(new_path)
                print(f"✓ [dir]  {d.name} -> {new_name}")

    files = list(path.rglob('*.md'))

    if not files:
        print(f"No .md files found in: {folder}")
        return

    renamed = 0
    changed = 0

    for file in files:
        content = file.read_text(encoding='utf-8')
        new_name = sanitize_filename(file.name)
        sanitized = sanitize_content(content, new_name)
        if sanitized != content:
            file.write_text(sanitized, encoding='utf-8')
            changed += 1

        if new_name != file.name:
            new_path = file.with_name(new_name)
            file.rename(new_path)
            print(f"✓ [file] {file.name} -> {new_name}" + (" (content changed)" if sanitized != content else ""))
            renamed += 1
        elif sanitized != content:
            print(f"✓ [file] {file.name} (content changed)")

    for d in path.rglob('*'):
        if d.is_dir():
            create_category_json(d, path)

    print(f"\nDone. Renamed: {renamed}, content changed: {changed}, total files: {len(files)}.")


if __name__ == '__main__':
    if len(sys.argv) < 2:
        print("Usage: python sanitize.py <folder>")
        sys.exit(1)
    sanitize_folder(sys.argv[1])
