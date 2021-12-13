# Read file and return array of lines
def read_file_as_lines(path)
  file = File.open(path)
  file.readlines.map(&:chomp)
end
